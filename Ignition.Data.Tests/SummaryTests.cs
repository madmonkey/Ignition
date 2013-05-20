
namespace Ignition.Data.Tests
{
    using System.Configuration;
    using Common.Data;
    using Entities;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class SummaryTests
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
                                                 .Select(g => new {Category = g.Key, Number = g.Count(c => true)}).ToList();
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
                //it's one to one from the dataset - but could select off of primary address
                var grouping = (r.Select(c => c.Addresses.FirstOrDefault())).ToList();
                unit.Commit();
                var areas = grouping.GroupBy(a => a.Country).Select(g => new {g.Key, Number = g.Count(c => true), Total = grouping.Count()});
                areas.Count().Should().BeGreaterOrEqualTo(1);
            }
        }

        [TestMethod]
        public void SelectTopCompanies()
        {
            const string searchBy = "a";
            var fh = new FluentHelper(false); //don't auto=configure as it will delete data
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new ReadOnlyRepository<CompanyEntity>(unit.Session);
                var e = r.Where(w => w.Name.Contains(searchBy)).Select(c => c).Take(5).ToList();
                unit.Commit();
                e.Count.ShouldBeEquivalentTo(5);
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
                const int pageNumber = 1;
                const int maxResults = 100;
                var audit = r.Select(a => a).Skip((pageNumber - 1)* maxResults).Take(maxResults);
                unit.Commit();
                audit.Count().Should().ShouldBeEquivalentTo(100);
            }
        }
    }

}