
namespace Ignition.Data.Tests
{
    using Common.Data;
    using Entities;
    using FluentAssertions;
    using Ignition.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class BasicData
    {
        [TestMethod]
        public void ThisDoesNothingButVerifyThatTheTestRunnerIsntHosed()
        {
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void CreateTables()
        {
            var fh = new FluentHelper(true);
            var sf = fh.CreateSessionFactory();
            sf.Should().NotBe(null);
        }

        [TestMethod]
        public void CanAddSearchAndDeleteCompany()
        {
            var fh = new FluentHelper(true);
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new Repository<CompanyEntity>(unit.Session);
                var c = new CompanyEntity {Name = "Ignition",Code = "IGNIT"};
                r.Add(c).ShouldBeEquivalentTo(true);
                unit.Commit();
            }

            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var r = new Repository<CompanyEntity>(unit.Session);
                var s = r.FirstOrDefault(e => e.Name == "Ignition");
                s.Should().NotBeNull("We just added it");
                r.Delete(s).Should().BeTrue("We just deleted it");
                unit.Commit(); //leave the database in a consistent state for testing
            }
        }

        [TestMethod]
        public void CanAddCompanyContactSearchAndDelete()
        {
            var fh = new FluentHelper(true);
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var companyRepo = new Repository<CompanyEntity>(unit.Session);
                var contactRepo = new Repository<ContactEntity>(unit.Session);
                var company = new CompanyEntity {Name = "Another Company",Code="ANOTCOMP"};
                companyRepo.SaveOrUpdate(company);
                var contact = new ContactEntity
                    {
                        Category = "VIP",
                        Company = company,
                        FirstName = "Andrew",
                        LastName = "Del Preore",
                        Title = "Software Developer",
                    };
                contact.Addresses = new List<AddressEntity>
                    {
                        new AddressEntity
                            {
                                Contact = contact,
                                Address = "10558 Via Del Luna",
                                City = "Orlando",
                                Country = "USA",
                                PostalCode = "32817",
                                Region = "SouthEast"
                            }
                    };
                contact.ContactInformation = new List<ContactInformationEntity>()
                    {
                        new ContactInformationEntity(){Contact = contact,ContactType="email", ContactValue="adelpreore@gmail.com"}
                    };
                contactRepo.SaveOrUpdate(contact).Should().BeTrue("Because we should be able to save");
                unit.Commit();
            }
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var companyRepo = new Repository<CompanyEntity>(unit.Session);
                var contactRepo = new Repository<ContactEntity>(unit.Session);
                var contacts = contactRepo.Select((c) => c).Where((contact) => contact.FirstName == "Andrew");
                var uniqueCompanies = (from c in contacts select c.Company).Distinct().ToList();
                uniqueCompanies.Count.Should().Be(1, "We only added one company");
                companyRepo.Delete(uniqueCompanies).Should().BeTrue("We should be able to do this"); //always "do" in batch when can!
                //Parallel.ForEach(uniqueCompanies, (c) => companyRepo.Delete(c));
                unit.Commit();
            }
        }
    }
}
