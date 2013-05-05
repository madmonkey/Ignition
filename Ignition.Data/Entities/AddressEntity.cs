
namespace Ignition.Data.Entities
{
    using System;

    public class AddressEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string Region { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual ContactEntity Contact { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
        
    }
}
