using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Property
{
    public class GetProprtyDetailsDto
    {
        public int Id { get; set; }
        public string PropertyId { get; set; }
        public string NameOfProperty { get; set; }
        public int? NoOfCounts { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string PropertyDescription { get; set; }
        public double? Price { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
