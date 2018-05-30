using System;

namespace TreeViewSandbox.Entities
{
    // ContactContactDetails
    public class ContactContactDetail
    {
        public int ContactDetailId { get; set; } // ContactDetailId (Primary key)
        public int ContactId { get; set; } // ContactId
        public int Type { get; set; } // Type
        public string Extension { get; set; } // Extension (length: 6)
        public string Detail { get; set; } // Detail (length: 50)
        public string Note { get; set; } // Note (length: 50)
        public byte IsExDirectory { get; set; } // IsExDirectory
        public byte IsPreferred { get; set; } // IsPreferred
        public DateTime Updated { get; set; } // Updated
        public byte IsInUse { get; set; } // IsInUse
        public string TimeToCall { get; set; } // TimeToCall (length: 3)
        public string ContactType { get; set; } // ContactType (length: 4)

        // Foreign Keys
        // This navigation property will not be lazy loaded due to the way this table is related to the Contacts Table.
        // The relationship exists in the X__IR table.
        public Contact Contact { get; set; }
    }
}
