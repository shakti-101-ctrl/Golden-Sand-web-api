using System;
using System.Collections.Generic;

namespace GoldenSand_WebAPI.Models
{
    public partial class NoticeDetailsTrash
    {
        public int Id { get; set; }
        public string NoticeId { get; set; }
        public string HeadingText { get; set; }
        public string Description { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
