using System;
using System.Collections.Generic;

namespace GoldenSand_WebAPI.Models
{
    public partial class TenantDetails
    {
        public int Id { get; set; }
        public string TenantId { get; set; }
        public string DuplexNumber { get; set; }
        public string TenantType { get; set; }
        public int? NoOfMembers { get; set; }
        public string Name { get; set; }
        public int? Contact { get; set; }
        public string Occupation { get; set; }
        public DateTime? StayingDate { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public bool? ApprovalStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public DuplexDetails DuplexNumberNavigation { get; set; }
    }
}
