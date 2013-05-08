
namespace Ignition.Services
{
    using Common.Data;
    using Data.Entities;
    using Ignition.Contracts;
    using ServiceStack.Common;
    using ServiceStack.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ISessionFactory = NHibernate.ISessionFactory;

    public class CompanyService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<Company> Get(Company company)
        {
            LogManager.LogFactory.GetLogger("").InfoFormat("Here {0}", DateTime.Now);
            var cacheKey = UrnId.Create<List<Company>>(string.Concat(Request.PathInfo,Request.QueryString));
            var cacheReturn = Cache.Get<List<Company>>(cacheKey);
            if (cacheReturn == null)
            {   
                using (var unit = new UnitOfWork(Factory.OpenSession()))
                {
                    //company.Id
                    var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                    var e = r.Where(w => company.Id.GetHashCode()==default(Guid).GetHashCode() || w.Id == company.Id).Select(c => c.TranslateTo<Company>()).ToList();
                    unit.Commit();
                    Cache.Set(cacheKey,e);
                    return e;
                }
            }
            return cacheReturn;
        }
    }
}