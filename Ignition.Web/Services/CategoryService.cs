﻿

namespace Ignition.Services
{
    using Common.Data;
    using Contracts;
    using Data.Entities;
    using NHibernate;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ServiceStack.ServiceInterface.Service
    {
        public ISessionFactory Factory { get; set; } //Injected by IOC
        public List<SummaryCategoryResponse> Get(SummaryCategory req)
        {
            using (var unit = new UnitOfWork(Factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                var grouping = (r.Select(c => c)).GroupBy(contact => contact.Category)
                                                    .Select(g => new SummaryCategoryResponse{ Category = g.Key, Number = g.Count(c => true) }).ToList();
                unit.Commit();
                var total = (from g in grouping select g.Number).Sum();
                return grouping.Select(c => new SummaryCategoryResponse { Category = c.Category, Number = c.Number, Total = total}).ToList();
            }
        }
    }
}