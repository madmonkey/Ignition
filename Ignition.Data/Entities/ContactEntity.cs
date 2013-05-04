
namespace Ignition.Data.Entities
{
    using System;

    public class ContactEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        public virtual Guid CompanyId { get; set; }
    }
}
