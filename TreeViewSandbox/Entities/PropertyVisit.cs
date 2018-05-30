using System;

namespace TreeViewSandbox.Entities
{
    // PropertyVisits
    public class PropertyVisit
    {
        public int ValuationId { get; set; } // ValuationId (Primary key)
        public int KfiId { get; set; } // KfiId
        public DateTime DateOfSurvey { get; set; } // DateOfSurvey
        public DateTime DateReportReceived { get; set; } // DateReportReceived
        public decimal ActualPropertyValue { get; set; } // ActualPropertyValue
    }
}
