using System;
using System.Collections.Generic;

namespace TreeViewSandbox.Entities
{
    // KeyFactsIllustrations
    public class KeyFactsIllustration
    {
        public int KfiId { get; set; } // KfiId (Primary key)
        public string UserReferenceNumber { get; set; } // UserReferenceNumber  (length: 10)
        public string IntroducerNumber { get; set; } // IntroducerNumber  (length: 10)
        public int BrokerContactId { get; set; } // BrokerContactId 
        public string LoanPlan { get; set; } // LoanPlan  (length: 5)
        public int PackagerMortgageId { get; set; } // PackagerMortgageId 
        public DateTime KfiDate { get; set; } // KfiDate 
        public DateTime ExpiryDate { get; set; } // ExpiryDate 
        public string KfiType { get; set; } // KfiType  (length: 1)
        public int CustomerId { get; set; } // CustomerId 
        public string Customer1Surname { get; set; } // Customer1Surname  (length: 50)
        public string Customer1Forename { get; set; } // Customer1Forename  (length: 50)
        public string Customer1Initials { get; set; } // Customer1Initials  (length: 50)
        public string Customer1Title { get; set; } // Customer1Title  (length: 4)
        public string Customer2Surname { get; set; } // Customer2Surname  (length: 50)
        public string Customer2Forename { get; set; } // Customer2Forename  (length: 50)
        public string Customer2Initials { get; set; } // Customer2Initials  (length: 5)
        public string Customer2Title { get; set; } // Customer2Title  (length: 4)
        public string Division { get; set; } // Division  (length: 2)
        public string CountryCode { get; set; } // CountryCode  (length: 1)
        public decimal InitialLoanAmountRequired { get; set; } // InitialLoanAmountRequired 
        public decimal ArrangementFee { get; set; } // ArrangementFee 
        public decimal CombinedAdviceFee { get; set; } // CombinedAdviceFee 
        public decimal EstimatedPropertyValue { get; set; } // EstimatedPropertyValue 
        public decimal IndividualBrokerFee { get; set; } // IndividualBrokerFee 
        public DateTime Customer1DateOfBirth { get; set; } // Customer1DateOfBirth 
        public string Customer1Gender { get; set; } // Customer1Gender  (length: 1)
        public DateTime Customer2DateOfBirth { get; set; } // Customer2DateOfBirth 
        public string Customer2Gender { get; set; } // Customer2Gender  (length: 1)
        public DateTime ApplicationReceivedDate { get; set; } // ApplicationReceivedDate 
        public string Part1LoanProduct { get; set; } // Part1LoanProduct  (length: 50)

        // Foreign Keys
        public virtual Contact BrokerContact { get; set; }
        //public virtual Origination Origination { get; set; }
        public virtual ICollection<Origination> Originations { get; set; }
        public virtual PropertySecurity PropertySecurity { get; set; }
    }
}
