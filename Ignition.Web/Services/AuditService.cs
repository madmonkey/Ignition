
namespace Ignition.Web
{
    using Common.Data;
    using Contracts;
    using Data.Entities;
    using NHibernate;
    using ServiceStack.Common;
    using System.Collections.Generic;
    using System.Linq;
    using ServiceStack.ServiceInterface;
    using ServiceStack.Text;

    [ClientCanSwapTemplates]
    [DefaultView("audit")]
    public class AuditService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<AuditResponse> Get(Audit req)
        {
            int pageNumber = 0;
            int maxResults = 50;
            if (Request.QueryString.HasKeys() && Request.QueryString["pg"] != null && Request.QueryString["pg"].IsInt())
            {
                pageNumber = Request.QueryString["pg"].ToInt(pageNumber);
            }
            if (Request.QueryString.HasKeys() && Request.QueryString["limit"] != null && Request.QueryString["limit"].IsInt())
            {
                maxResults = Request.QueryString["limit"].ToInt(maxResults);
            }

            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<AuditEntity>(unit.Session);
                List<AuditResponse> recs = null;
                if (pageNumber >= 0)
                {
                    recs =
                        r.OrderByDescending(a => a.Id)
                         .Select(a => a.TranslateTo<AuditResponse>())
                         .Skip((pageNumber - 1)*maxResults)
                         .Take(maxResults)
                         .ToList();
                }
                else
                {
                    //move last - reorder so appears in similar "descending" order
                    recs =
                        r.OrderBy(a => a.Id)
                         .Select(a => a.TranslateTo<AuditResponse>())
                         .Take(maxResults).ToList().OrderByDescending(a=> a.Id).ToList();
                }
                unit.Commit();
                return recs;
            }
        }
    }

}