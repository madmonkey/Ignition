
namespace Ignition.Web.Services
{
    using Common.Data;
    using Contracts;
    using Data.Entities;
    using NHibernate;
    using ServiceStack.Common;
    using ServiceStack.Logging;
    using System;
    using System.Linq;

    public class SummaryService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public SummaryResponse Get(Summary summary)
        {
            LogManager.LogFactory.GetLogger("").InfoFormat("Here {0}", DateTime.Now);
            var cacheKey = UrnId.Create<SummaryResponse>("Summaries");
            var cacheReturn = Cache.Get<SummaryResponse>(cacheKey);
            var summaryResponse = new SummaryResponse();
            if (cacheReturn == null)
            {
                using (var unit = new UnitOfWork(Factory.OpenSession()))
                {
                    var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                    var g = r.Select(c => new SummaryCategoryResponse() {Category = c.Category}).ToList();
                    var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                    unit.Commit();
                    summaryResponse.ByCategory =
                        g.GroupBy(ce => ce.Category)
                         .Select(
                             ceg =>
                                 {
                                     var summaryCategoryResponse = ceg.FirstOrDefault();
                                     return summaryCategoryResponse != null
                                                ? new SummaryCategoryResponse()
                                                    {
                                                        Category = summaryCategoryResponse.Category,
                                                        Number = ceg.Sum(i => 1),
                                                        Total = g.Count(),
                                                        Percentage = (double) (ceg.Sum(i => 1)*100)/g.Count()
                                                    }
                                                : null;
                                 }).ToList();
                    
                    var areas = grouping.GroupBy(a => a.Country)
                                .Select(gr => new { gr.Key, Number = gr.Count(c => true), Total = grouping.Count() });
                    summaryResponse.ByLocation =
                        areas.Select(c => new SummaryLocationResponse
                        {
                            Country = c.Key,
                            Number = c.Number,
                            Total = c.Total,
                            Percentage = (double)(c.Number * 100) / c.Total
                        }).ToList();
                }
                //using (var unit = new UnitOfWork(Factory.OpenSession()))
                //{
                //    var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                //    //it's a one to one from the dataset - but could select off of a primary address property
                //    var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                //    unit.Commit();
                //    var areas =
                //        grouping.GroupBy(a => a.Country)
                //                .Select(g => new {g.Key, Number = g.Count(c => true), Total = grouping.Count()});
                //    summaryResponse.ByLocation =
                //        areas.Select(c => new SummaryLocationResponse
                //            {
                //                Country = c.Key,
                //                Number = c.Number,
                //                Total = c.Total,
                //                Percentage = (double) (c.Number*100)/c.Total
                //            }).ToList();
                //}

                return summaryResponse;
            }
            return cacheReturn;
        }
    }
}