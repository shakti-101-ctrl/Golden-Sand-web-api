using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Duplex
{
    public class GetDuplexDetailsDto
    {
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
    }
}
