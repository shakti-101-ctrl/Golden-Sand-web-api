using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Notice
{
    public class AddNoticeDto
    {

        public string NoticeId { get; set; }
        //[Required]
        public string HeadingText { get; set; }
        //[Required]
        public string Description { get; set; }
        //[Required]
        public DateTime? PostedDate { get; set; }
        //[Required]
        public DateTime? EndDate { get; set; }
        public bool? ActiveStatus { get; set; } = true;
        public bool? DeleteStatus { get; set; } = false;
        public DateTime? EntryDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
