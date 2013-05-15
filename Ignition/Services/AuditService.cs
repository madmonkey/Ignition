
namespace Ignition.Services
{
    using Common.Data;
    using Contracts;
    using Data.Entities;
    using NHibernate;
    using ServiceStack.Common;
    using System.Collections.Generic;
    using System.Linq;

    public class AuditService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<AuditResponse> Get(Audit req)
        {
            int pageNumber = 0;
            int maxResults = 50;
            if (Request.QueryString.HasKeys() && Request.QueryString["pg"] != null && Request.QueryString["pg"].IsInt())
            {
                pageNumber = Request.QueryString["pg"].ToInt(0);
            }
            if (Request.QueryString.HasKeys() && Request.QueryString["limit"] != null && Request.QueryString["limit"].IsInt())
            {
                maxResults = Request.QueryString["limit"].ToInt(50);
            }

            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<AuditEntity>(unit.Session);
                var recs = r.Select(a => a.TranslateTo<AuditResponse>()).Skip((pageNumber - 1)* maxResults).Take(maxResults).ToList();
                unit.Commit();
                return recs;
            }
        }
    }
}