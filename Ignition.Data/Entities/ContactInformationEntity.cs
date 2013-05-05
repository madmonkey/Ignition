
namespace Ignition.Data.Entities
{
    using System;

    public class ContactInformationEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual ContactEntity Contact { get; set; }
        public virtual string ContactType { get; set; }
        public virtual string ContactValue { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
    }
}
