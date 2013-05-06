
namespace Ignition.Services
{
    using System;
    using Common.Data;
    using Data.Entities;
    using Ignition.Contracts;
    using NHibernate;
    using ServiceStack.Common;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.Logging;
    using ServiceStack.Logging.Support.Logging;

    public class CompanyService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<Company> Get(Company company)
        {
            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                LogManager.LogFactory.GetLogger("").InfoFormat("Here {0}", DateTime.Now);
                var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                var comp = r.Select(c => c.TranslateTo<Company>());
                unit.Commit();
                return comp.ToList();
            }
        }
    }
}