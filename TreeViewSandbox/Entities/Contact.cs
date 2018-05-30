using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TreeViewSandbox.DupesFromArtemis;

namespace TreeViewSandbox.Entities
{
    // Contacts
    public class Contact
    {
        public int ContactId { get; set; } // ContactId (Primary key)
        public string Surname { get; set; } // Surname (length: 50)
        public string Forename { get; set; } // Forename (length: 50)
        public string Title { get; set; } // Title (length: 4)
        public int BrokerFirmId { get; set; } // BrokerFirmId
        public string Name { get; set; } // Name (length: 20)
        public string UserReferenceNumber { get; set; } // UserReferenceNumber (length: 10)
        public string Status { get; set; } // Status (length: 1)

        [Column("BDM")]
        public string Bdm { get; set; } // BDM (length: 4)

        // Foreign Keys
        public virtual Broker Broker { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public ICollection<ContactContactDetail> ContactDetails { get; set; }
        public virtual ICollection<KeyFactsIllustration> Kfis { get; set; } = new List<KeyFactsIllustration>();
    }
}
