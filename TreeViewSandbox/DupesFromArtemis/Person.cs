using System;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    // Persons
    public class Person
    {
        public int ContactId { get; set; } // ContactId (Primary key)
        public byte Sequence { get; set; } // Sequence
        public int CustomerId { get; set; } // CustomerId
        public DateTime DateOfBirth { get; set; } // DateOfBirth
        public DateTime DateOfDeath { get; set; } // DateOfDeath
        public string Gender { get; set; } // Gender (length: 1)

        // Foreign Keys
        // According to the er diagram sent to us by Pure, this navigation property will not be lazy loaded due to the way this table 
        // is related to the Customers Table. This relationship exists in the X__IR table.
        // So you would join through the X__IR table to obtain the internal ref id to find the customer id.
        public virtual Customer Customer { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
