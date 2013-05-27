

namespace Ignition.Contracts
{
    using System.Collections.Generic;
    using ServiceStack.ServiceHost;
    using System;

    [Api("GET or DELETE a single entity by Id. Use POST to create a new and PUT to update it")]
    [Route("/customers", "POST,PUT,PATCH,DELETE")]
    [Route("/customers/{id}")]
    public class Company : Request
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }

    public class CompanyResponse : Response
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<Contact> Contacts { get; set; }
    }

    public class Contact : Response
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<AddressInformation> Addresses { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }

    public class ContactInformation : Response
    {
        public Guid Id { get; protected set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }

    public class AddressInformation : Response
    {
        public virtual Guid Id { get; protected set; }
        public string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
    }
    
}
