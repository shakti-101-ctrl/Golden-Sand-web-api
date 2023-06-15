using GoldenSand_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Tenant
{
    public class AddTenantDetailsDto
    {
       
        public string TenantId { get; set; }
        public string DuplexNumber { get; set; }
        public string TenantType { get; set; }
        public int? NoOfMembers { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Occupation { get; set; }
        public DateTime? StayingDate { get; set; }
        public bool? ActiveStatus { get; set; } = true;
        public bool? DeleteStatus { get; set; } = false;
        public bool? ApprovalStatus { get; set; } = false;
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DuplexDetails DuplexNumberNavigation { get; set; }
    }
}
