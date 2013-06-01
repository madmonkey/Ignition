
namespace Ignition.Web.Services
{
    using Common.Data;
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
        public List<CompanyResponse> Get(Company company)
        {
            LogManager.LogFactory.GetLogger("").InfoFormat("Here {0}", DateTime.Now);
            if (!Request.QueryString.HasKeys())
            {
                return new List<CompanyResponse>(){new CompanyResponse()};
            }
            if(Request.QueryString["name"] != null)
            {
                return Get(Request.QueryString["name"]);
            }
            
            var cacheKey = UrnId.Create<List<CompanyResponse>>(string.Concat(Request.PathInfo, Request.QueryString["id"]));
            var cacheReturn = Cache.Get<List<CompanyResponse>>(cacheKey);
            if (cacheReturn == null)
            {
                using (var unit = new UnitOfWork(Factory.OpenSession()))
                {
                    var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                    var e = r.Where(w => company.Id.Equals(ReflectionUtils.GetDefaultValue(company.Id.GetType())) || w.Id == company.Id)
                        .OrderBy(c => c.Name)
                        .Select(Transpose).ToList();
                    unit.Commit();
                    Cache.Set(cacheKey, e);
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
        public List<CompanyResponse> Get(string searchBy)
        {
            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                var e = r.Where(w => w.Name.Contains(searchBy)).Select(c => c).Take(5).ToList();
                unit.Commit();
                return e.Select(Transpose).ToList();
            }
        }

        /// <summary>
        /// Transposes the specified complex entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private static CompanyResponse Transpose(CompanyEntity entity)
        {
            return new CompanyResponse
                {
                    Code = entity.Code,
                    Id = entity.Id,
                    Name = entity.Name,
                    Contacts = entity.Contacts.Select(cs => new Contact
                        {
                            Id = cs.Id,
                            Category = cs.Category,
                            FirstName = cs.FirstName,
                            LastName = cs.LastName,
                            Title = cs.Title,
                            Addresses = cs.Addresses.Select(a=> a.TranslateTo<AddressInformation>()).ToList(),
                            ContactInformation = cs.ContactInformation.Select(ci=> ci.TranslateTo<ContactInformation>()).ToList()
                        }).ToList()
                };
        }
    }
}