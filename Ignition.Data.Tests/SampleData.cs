
namespace Ignition.Data.Tests
{
    using Common.Data;
    using Entities;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SampleData
    {
        [TestMethod]
        public void LoadSampleData()
        {
            var fh = new FluentHelper(false);
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var companyRepo = new Repository<CompanyEntity>(unit.Session);
                var reader = new StreamReader(File.OpenRead(@"SampleData.csv"));
                reader.ReadLine(); //skip headers
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split(',');
                        var c = new CompanyEntity
                            {
                                Code = values[0],
                                Name = values[1],
                            };
                        c.Contacts = new List<ContactEntity>
                            {
                                new ContactEntity
                                    {
                                        Category = values[11],
                                        FirstName = values[2].Substring(0, values[2].IndexOf(" ", System.StringComparison.Ordinal)),
                                        LastName = values[2].Substring(values[2].IndexOf(" ", System.StringComparison.Ordinal) + 1),
                                        Title = values[3],
                                        Company = c
                                    }
                            };
                        foreach (var contact in c.Contacts)
                        {
                            contact.Addresses = new List<AddressEntity>
                                {
                                    new AddressEntity
                                        {
                                            Address = values[4].Replace("\"",string.Empty),
                                            Contact = contact,
                                            City = values[5],
                                            Region = values[6],
                                            PostalCode = values[7],
                                            Country = values[8]
                                        }
                                };
                            contact.ContactInformation = new List<ContactInformationEntity>
                                {
                                    new ContactInformationEntity()
                                        {
                                            Contact = contact,
                                            ContactType = "phone",
                                            ContactValue = values[9]
                                        },
                                    new ContactInformationEntity()
                                        {
                                            Contact = contact,
                                            ContactType = "fax",
                                            ContactValue = values[10]
                                        }
                                };
                        }
                        companyRepo.SaveOrUpdate(c).Should().BeTrue("Saving within a large transaction");
                    }
                }
                unit.Commit();
            }
        }

        [TestMethod]
        public void RemoveAllSampleData()
        {
            var fh = new FluentHelper(false);
            var factory = fh.CreateSessionFactory();
            using (var unit = new UnitOfWork(factory.OpenSession()))
            {
                var companyRepo = new Repository<CompanyEntity>(unit.Session);
                var companies = companyRepo.Select(c => c).ToList();
                companyRepo.Delete(companies).Should().BeTrue("Because we should");
                //Parallel.ForEach(companies, c => companyRepo.Delete(c).Should().BeTrue("Should be able to remove in batch"));
                unit.Commit();
            }
        }
    }
}
