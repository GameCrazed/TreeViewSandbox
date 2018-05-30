using System.Collections.Generic;

namespace TreeViewSandbox.Entities
{
    // BrokerFirms
    public class Broker
    {
        public int BrokerFirmId { get; set; } // BrokerFirmId (Primary key)
        public string FcaNumber { get; set; } // FCANumber (length: 10)
        public int NetworkFcaNumber { get; set; } // NetworkFCANumber
        public string BrokerTypeFlag { get; set; } // BrokerTypeFlag (length: 1)
        public string MortgageCommissionBand { get; set; } // MortgageCommissionBand (length: 1)
        public string ProductFilter { get; set; } // ProductFilter (length: 10)
        public string Commission { get; set; } // Commission (length: 6)

        public string BankAccountSortCode => Customer.BankAccountSortCode;
        public int BankAccountNumber => Customer.BankAccountNumber;

        // Foreign Keys
        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public virtual Customer Customer { get; set; }
    }
}
