using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.DTOs.Admin.Meeting
{
    public class UpdateMeetingDetailsDto
    {
        public int Id { get; set; }
        public string MeetingId { get; set; }
        public string HeadingText { get; set; }
        public string Description { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? MeetStartDateTime { get; set; }
        public DateTime? MeetEndDateTime { get; set; }
        public string InvitedPersons { get; set; }
        public bool? ActiveStatus { get; set; }
        public bool? DeleteStatus { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? LasteModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
