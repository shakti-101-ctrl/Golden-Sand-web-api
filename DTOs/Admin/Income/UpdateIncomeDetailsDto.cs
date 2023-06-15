﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Income
{
    public class UpdateIncomeDetailsDto
    {
        public int Id { get; set; }
        public string IncomeId { get; set; }
        public string DuplexNumber { get; set; }
        public string Purpose { get; set; }
        public string PaymentType { get; set; }
        public string TowhomOrTransactionId { get; set; }
        public string OwnerName { get; set; }
        public DateTime? DateEntry { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Narration { get; set; }
        public bool? ActiveStatus { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}
