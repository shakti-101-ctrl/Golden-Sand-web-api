using GoldenSand_WebAPI.DTOs.Admin.Meeting;
using GoldenSand_WebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        
        private readonly IAdminRespository _repos;
        public MeetingController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMeetings()
        {
            return Ok(await _repos.GetAllMeetings());
        }

        // GET: api/Brand/5
        [HttpGet("{meetingid}")]
        public async Task<IActionResult> GetMeetingByMeetingId(string meetingid)
        {
            return Ok(await _repos.GetMeetingByMeetingId(meetingid));
        }
        [HttpPost]
        public async Task<IActionResult> PostMeeting([FromBody] AddMeetingDetailsDto addMeeting)
        {
            //if (!ModelState.IsValid)
            //{
            //    return UnprocessableEntity(ModelState);
            //}
            return Ok(await _repos.AddMeetingDetails(addMeeting));
        }

        [HttpDelete("{meetingid}")]
        public async Task<IActionResult> DeleteNotice([FromRoute] string meetingid)
        {
            return Ok(await _repos.DeleteMeetingDetails(meetingid));
        }

        [HttpPut("{meetingid}")]
        public async Task<IActionResult> UpdateNoticeDetails(string meetingid, [FromBody] UpdateMeetingDetailsDto update)
        {

            return Ok(await _repos.UpdateMeetinfDetails(meetingid, update));
        }
    }
}
