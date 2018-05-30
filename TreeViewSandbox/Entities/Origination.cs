using System;
using System.Linq;

namespace TreeViewSandbox.Entities
{
    // Originations
    public class Origination
    {
        public int OriginationId { get; set; } // OriginationId (Primary key)
        public int CustomerNumber { get; set; } // CustomerNumber 
        public int KfiId { get; set; } // KfiId
        public string DivisionCode { get; set; } // DivisionCode  (length: 2)
        public DateTime DateKfiCreated { get; set; } // DateKfiCreated 
        public DateTime StatusChangeDate { get; set; } // StatusChangeDate 
        public int ApplicationNumber { get; set; } // ApplicationNumber 
        public DateTime OfferDate { get; set; } // OfferDate 
        public DateTime CompleteDate { get; set; } // CompleteDate 
        public int OfferNumber { get; set; } // OfferNumber 
        public string PrimaryStatus { get; set; } // PrimaryStatus  (length: 1)
        public string SecondaryStatus { get; set; } // SecondaryStatus  (length: 1)
        public decimal CashAdvance { get; set; } // CashAdvance 
        public decimal ArrangementFee { get; set; } // ArrangementFee 
        public string CompFeePaymentMethod { get; set; } // CompFeePaymentMethod  (length: 1)
        public decimal TotalFees { get; set; } // TotalFees 
        public DateTime DateApplicationReceived { get; set; } // DateApplicationReceived 
        public DateTime DateCotReceived { get; set; } // DateCOTReceived 

        // Foreign Keys
        public virtual Account Account { get; set; }
        public virtual KeyFactsIllustration KeyFactsIllustration { get; set; }


        public char PrimaryStatusChar {
            get {
                var c = PrimaryStatus?.ToCharArray().SingleOrDefault();
                return c.HasValue ? c.Value : default(char);
            }
        }
    }
}
