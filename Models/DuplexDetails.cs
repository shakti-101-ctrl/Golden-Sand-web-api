using System;
using System.Collections.Generic;

namespace GoldenSand_WebAPI.Models
{
    public partial class DuplexDetails
    {
        public DuplexDetails()
        {
            IncomeDetails = new HashSet<IncomeDetails>();
            TenantDetails = new HashSet<TenantDetails>();
        }

        public int Id { get; set; }
        public string DuplexId { get; set; }
        public string DuplexNumber { get; set; }
        public string OwnerName { get; set; }
        public string AdharCardCopy { get; set; }
        public string PhotoCopy { get; set; }
        public string Contact { get; set; }
        public string AlternateContact { get; set; }
        public string EmailId { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<IncomeDetails> IncomeDetails { get; set; }
        public ICollection<TenantDetails> TenantDetails { get; set; }
    }
}
