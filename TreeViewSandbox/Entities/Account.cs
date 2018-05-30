using System;

namespace TreeViewSandbox.Entities
{
    public class Account
    {
        public int OriginationId { get; set; } // OriginationId (Primary key)
        public int CustomerId { get; set; } // CustomerId
        public DateTime CompletionDate { get; set; } // CompletionDate
        public int ProductId { get; set; } // ProductId
        public int AccountNo { get; set; } // AccountNo
        public decimal OriginalLoan { get; set; } // OriginalLoan
        public int BrokerContactId { get; set; } // BrokerContactId

        // Foreign Keys
        public virtual Origination Origination { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Contact BrokerContact { get; set; }
        public virtual Product Product { get; set; }
    }
}
