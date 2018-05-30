using System.Collections.Generic;
using TreeViewSandbox.DupesFromArtemis;

namespace TreeViewSandbox.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } // CustomerId (Primary key)
        public string BankAccountSortCode { get; set; } // BankAccountSortCode (length: 6)
        public int BankAccountNumber { get; set; } // BankAccountNumber
        public string ShortName { get; set; } // ShortName (length: 20)

        // Foreign Keys
        public virtual Broker Broker { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}
