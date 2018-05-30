using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeViewSandbox.Entities
{
    // Addresses
    public class Address
    {
        public int AddressId { get; set; } // AddressId (Primary key)

        [Column("OtherEntityId")]
        public int CustomerId { get; set; } // OtherEntityId 
        public string EntityName { get; set; } // EntityName (length: 4)
        public int ContactId { get; set; } // ContactId 
        public System.DateTime LastWriteDate { get; set; } // LastWriteDate 
        public string LastWritePerson { get; set; } // LastWritePerson (length: 4)
        public int Type { get; set; } // Type 
        public string Name { get; set; } // Name (length: 75)
        public string CareOf { get; set; } // CareOf (length: 75)
        public string Street { get; set; } // Street (length: 75)
        public string District { get; set; } // District (length: 75)
        public string Town { get; set; } // Town (length: 30)
        public string County { get; set; } // County (length: 20)

        [Column("PostCodeAndCounty")]
        public string PostCode { get; set; } // PostCodeAndCounty (length: 20)
        public string CountryCode { get; set; } // CountryCode (length: 4)
        public string PostcodeAndStreet { get; set; } // PostcodeAndStreet (length: 100)
        public byte DoNotSentCorrespondence { get; set; } // DoNotSentCorrespondence 

        // Foreign Keys
        public virtual ICollection<PropertySecurity> PropertySecurity { get; set; }
    }
}
