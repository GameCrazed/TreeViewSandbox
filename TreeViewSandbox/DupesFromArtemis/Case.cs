using System;
using System.Collections.Generic;
using System.Text;
using TreeViewSandbox.Entities;

namespace TreeViewSandbox.DupesFromArtemis
{
    public class Case
    {
        private static DateTime PhoebusMinDate { get; } = new DateTime(1900, 1, 1, 0, 0, 0);
        public int ApplicationNumber { get; set; }
        public int KfiId { get; set; }
        public int StateId { get; set; }
        public string CustomerSurname { get; set; }
        public CaseState CaseState { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public DateTime? OfferDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? WithdrawnDate { get; set; }
        public DateTime? DeclinedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? KfiDate { get; set; }
        public int OfferNumber { get; set; }
        public int PackagerMortgageId { get; set; }
        public string PrimaryStatus { get; set; }
        public DateTime DateApplicationReceived { get; set; }
        public DateTime? ValuationDate { get; set; }
        public string PoR { get; set; }
        public string FullAddress { get; set; }
        public Person ClientOne { get; set; }
        public Person ClientTwo { get; set; }
        public Contact ClientOneContact { get; set; }
        public Contact ClientTwoContact { get; set; }
        public IEnumerable<ContactContactDetail> ClientOneContactDetail { get; set; }
        public IEnumerable<ContactContactDetail> ClientTwoContactDetail { get; set; }
        public int CustomerId { get; set; }
        public string KfiType { get; set; }
        public string Division { get; set; }
        public bool KfiOnly { get; set; }
        public string PostCode { get; set; }


        public Case()
        {

        }

        static DateTime? PhoebusMinDateToNull(DateTime dateTime)
        {
            return dateTime == PhoebusMinDate ? default(DateTime?) : dateTime;
        }

        public Case(CaseStateHistory history, Origination origination, Account account, KeyFactsIllustration kfi)
        {
            if (origination != null)
            {
                CustomerId = origination.CustomerNumber;
                KfiId = origination.KfiId;
                ApplicationNumber = origination.ApplicationNumber;
                CreatedDate = PhoebusMinDateToNull(origination.DateKfiCreated);
                DeclinedDate = origination.PrimaryStatusChar == OriginationStatus.Rejected ? PhoebusMinDateToNull(origination.StatusChangeDate) : null;
                OfferDate = PhoebusMinDateToNull(origination.OfferDate);
                SubmittedDate = PhoebusMinDateToNull(origination.DateApplicationReceived);
                WithdrawnDate = origination.PrimaryStatusChar == OriginationStatus.Withdrawn ? PhoebusMinDateToNull(origination.StatusChangeDate) : null;
                OfferNumber = origination.OfferNumber;
                PrimaryStatus = origination.PrimaryStatus;
                DateApplicationReceived = origination.DateApplicationReceived;
            }

            if (origination != null && account != null)
            {
                CompletionDate = origination.PrimaryStatusChar == OriginationStatus.Complete ? PhoebusMinDateToNull(account.CompletionDate) : null;
            }

            if (kfi != null)
            {
                CustomerSurname = kfi.Customer1Surname;
                KfiId = kfi.KfiId;
                ExpiredDate = PhoebusMinDateToNull(kfi.ExpiryDate);
                KfiDate = PhoebusMinDateToNull(kfi.KfiDate);
                PackagerMortgageId = kfi.PackagerMortgageId;
                KfiType = kfi.KfiType;
                Division = kfi.Division;

                if (origination == null && account == null)
                {
                    ClientOne = new Person { DateOfBirth = kfi.Customer1DateOfBirth };
                    ClientTwo = new Person { DateOfBirth = kfi.Customer2DateOfBirth };

                    ClientOneContact = new Contact { Title = kfi.Customer1Title, Forename = kfi.Customer1Forename, Surname = kfi.Customer1Surname };
                    ClientTwoContact = new Contact { Title = kfi.Customer2Title, Forename = kfi.Customer2Forename, Surname = kfi.Customer2Surname };

                    KfiOnly = true;
                }
            }

            if (history != null)
            {
                KfiId = history.KfiId;
                StateId = history.CaseStateId;
                CaseState = history.CaseState;
            }
        }

        public Case(CaseStateHistory history,
            Origination origination,
            Account account,
            KeyFactsIllustration kfi,
            PropertyVisit visit,
            AddressSelection address) : this(history, origination, account, kfi)
        {
            ValuationDate = visit?.DateOfSurvey;

            PoR = BuildPoRLabel(address);

            FullAddress = BuildAddressLabel(address);

            PostCode = address?.PostCode;
        }

        private string BuildPoRLabel(AddressSelection address)
        {
            if (address == null || address.CustomerAddressId < 1)
            {
                return "No address data found.";
            }

            return address?.CustomerAddressId == address?.PropertySecurityAddressId
                ? "Re-mortgage"
                : "Purchase";
        }

        private string BuildAddressLabel(AddressSelection address)
        {
            if (string.IsNullOrWhiteSpace(address?.Street))
                return "No address data found.";

            var builder = new StringBuilder();

            if (address.Street != null)
            {
                builder.AppendFormat("{0}, ", address.Street.Trim());
            }

            if (!string.IsNullOrWhiteSpace(address.District))
            {
                builder.AppendFormat("{0}, ", address.District.Trim());
            }

            if (!string.IsNullOrWhiteSpace(address.County))
            {
                builder.AppendFormat("{0}, ", address.County.Trim());
            }

            if (!string.IsNullOrWhiteSpace(address.Region))
            {
                builder.AppendFormat("{0}, ", address.Region.Trim());
            }

            if (address.PostCode != null)
            {
                builder.AppendFormat("{0}.", address.PostCode.Trim());
            }

            return builder.ToString();
        }
    }
}



