using System;
using System.Collections.Generic;

namespace GoldenSand_WebAPI.Models
{
    public partial class ExpenseDetails
    {
        public int Id { get; set; }
        public string ExpenseId { get; set; }
        public string ExpenseHead { get; set; }
        public string PaymentType { get; set; }
        public string TowhomOrTransactionId { get; set; }
        public DateTime? DateEntry { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Narration { get; set; }
        public bool? ActiveStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
