
namespace Ignition.Services
{
    using Common.Data;
    using Data;
    using Data.Entities;
    using Ignition.Contracts;
    using ServiceStack.Common;
    using ServiceStack.Common.Utils;
    using ServiceStack.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ISessionFactory = NHibernate.ISessionFactory;

    public class CompanyService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC

        /// <summary>
        /// Gets the specified company.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        public List<Company> Get(Company company)
        {
            LogManager.LogFactory.GetLogger("").InfoFormat("Here {0}", DateTime.Now);
            if (Request.QueryString.HasKeys() && Request.QueryString["Name"] != null)
                return Get(Request.QueryString["Name"]);
            var cacheKey = UrnId.Create<List<Company>>(string.Concat(Request.PathInfo, Request.QueryString));
            var cacheReturn = Cache.Get<List<Company>>(cacheKey);
            if (cacheReturn == null)
            {   
                using (var unit = new UnitOfWork(Factory.OpenSession()))
                {
                    var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                    var e = r.Where(w => company.Id.Equals(ReflectionUtils.GetDefaultValue(company.Id.GetType())) || w.Id == company.Id)
                        .Select(c => c.TranslateTo<Company>()).ToList();
                    unit.Commit();
                    Cache.Set(cacheKey,e);
                    return e;
                }
            }
            return cacheReturn;
        }

        /// <summary>
        /// Gets the company by the specified search by.
        /// </summary>
        /// <param name="searchBy">The search by.</param>
        /// <returns></returns>
        public List<Company> Get(string searchBy)
        {
            //var fh = new FluentHelper(false); //don't auto=configure as it will delete data
            //var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                var e = r.Where(w => w.Name.Contains(searchBy)).Select(c => c).Take(5).ToList();
                unit.Commit();
                return e.Select(c => c.TranslateTo<Company>()).ToList();
                //var e = r.Where(w => w.Name.Contains(searchBy)).Select(c => c.TranslateTo<Company>()).ToList();
                //return e.Take(5).ToList();
            }
        }
    }
}