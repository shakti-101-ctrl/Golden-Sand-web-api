using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Employee
{
    public class UpdateEmployeeDetailsDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public string Contact { get; set; }
        public string AddressDetails { get; set; }
        public string Photo { get; set; }
        public string ScanCopyOfAdharCard { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string ProviderName { get; set; }
        public string ProviderOwnerName { get; set; }
        public string ProviderAddressdetails { get; set; }
        public string ProviderContact { get; set; }
        public string ProviderAlternateContact { get; set; }
        public string RegistrationNumber { get; set; }
        public bool? ActiveStatus { get; set; } = true;
        public bool? DeleteStatus { get; set; } = false;
        public DateTime? EntryDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; } = DateTime.Now;
        public DateTime? DeleteDate { get; set; }
    }
}
