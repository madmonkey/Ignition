﻿
namespace Ignition.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using Common.Data;

    /// <summary>
    /// A system needs to focus around people - Contact is the main entity
    /// </summary>
    /// <see cref="http://www.hanselman.com/blog/CommunityCallToActionNOTNorthwind.aspx"/>
    public class ContactEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        public virtual string Category { get; set; }
        public virtual CompanyEntity Company { get; set; }
        public virtual IList<AddressEntity> Addresses { get; set; }
        public virtual IList<ContactInformationEntity> ContactInformation { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
    }
}
