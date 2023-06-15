using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Notice
{
    public class UpdateNoticeDto
    {
        public int Id { get; set; }
        public string NoticeId { get; set; }
        public string HeadingText { get; set; }
        public string Description { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? ActiveStatus { get; set; } = true;
        public bool? DeleteStatus { get; set; } = false;
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
