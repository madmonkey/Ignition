using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ignition.Data.Tests
{
    using Common.Data;
    using Entities;
    using System.Linq;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void QueryByCategory()
        {
            var fh = new FluentHelper(false); //don't auto=configure as it will delete data
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                var grouping = (r.Select(c => c)).GroupBy(contact => contact.Category)
                                                 .Select(g => new {Category = g.Key, Sum = g.Count(c => true)}).ToList();
                unit.Commit();
                Console.WriteLine(grouping.Count);

            }
        }

        [TestMethod]
        public void QueryByLocation()
        {
            var fh = new FluentHelper(false); //don't auto=configure as it will delete data
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<ContactEntity>(unit.Session);
                //it's one to one from data - but could select off of primary address
                var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                unit.Commit();
                var areas = grouping.GroupBy(a => a.Country).Select(g => new {g.Key, Total = g.Count(c => true)});
            }
        }

        [TestMethod]
        public void AuditQuery()
        {
            var fh = new FluentHelper(false); //don't auto=configure as it will delete data
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<AuditEntity>(unit.Session);
                var pageNumber = 1;
                var maxResults = 100;
                var currentCount = 0;
                var audit = r.Select(a => a).Skip((pageNumber - 1)* maxResults).Take(maxResults);
                unit.Commit();
                Console.WriteLine(audit.Count());
                //it's one to one from data - but could select off of primary address
                //var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                //unit.Commit();
                //var areas = grouping.GroupBy(a => a.Country).Select(g => new { g.Key, Total = g.Count(c => true) });
            }
        }
          
    }

}