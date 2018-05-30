using System.ComponentModel.DataAnnotations.Schema;

namespace TreeViewSandbox.Entities
{
    // PropertySecurities
    public class PropertySecurity
    {
        [Column("PRSR010")]
        public int KfiId { get; set; } // PRSR010 (Primary key)
        public decimal ValuationResult { get; set; } // ValuationResult
        public int SecurityAddressId { get; set; } // SecurityAddressId
        public string Region { get; set; } // Region (length: 40)

        // Foreign Keys
        public virtual KeyFactsIllustration KeyFactsIllustration { get; set; }
        public virtual Address Address { get; set; }
    }
}
