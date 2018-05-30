using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeViewSandbox.Entities
{
    // Products
    public class Product
    {
        public int ProductId { get; set; } // ProductId (Primary key)

        [Column("PROD050")]
        public string Division { get; set; } // PROD050  (length: 2)
        public decimal InterestRate { get; set; } // InterestRate 
        public string ProductName { get; set; } // ProductName  (length: 2000)
        public string ProductCode { get; set; } // ProductCode  (length: 50)
        public DateTime LastAppCreateDate { get; set; } // LastAppCreateDate 
        public DateTime LastKfiCreateDate { get; set; } // LastKfiCreateDate 
        public string ProductDescription { get; set; } // ProductDescription  (length: 150)

        // Foreign Keys
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
