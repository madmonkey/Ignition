
namespace Ignition.Services
{
    using Common.Data;
    using Contracts;
    using Data.Entities;
    using NHibernate;
    using System.Collections.Generic;
    using System.Linq;

    public class LocationService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<SummaryLocationResponse> Get(SummaryLocation req)
        {
            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                //it's one to one from data - but could select off of a primary address -> like where IsPrimary
                var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                unit.Commit();
                var total = (from g in grouping select 1).Sum();
                return
                    grouping.GroupBy(a => a.Country)
                            .Select(
                                g => new SummaryLocationResponse {Country = g.Key, Number = g.Count(c => true), Total = total})
                            .ToList();
            }
        }
    }
}