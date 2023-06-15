using GoldenSand_WebAPI.DTOs.Admin.Events;
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
    public class EventController : ControllerBase
    {
        ServiceResponse<IActionResult> service = new ServiceResponse<IActionResult>();
        private readonly IAdminRespository _repos;
        public EventController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEvent()
        {
            return Ok(await _repos.GetAllEvents());
        }

        // GET: api/Brand/5
        [HttpGet("{eventid}")]
        public async Task<IActionResult> GetAllEvntByEventId(string eventid)
        {
            return Ok(await _repos.GetEventById(eventid));
        }
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] AddEventsDto addEventsDto)
        {
            //if (!ModelState.IsValid)
            //{
              //  return UnprocessableEntity(ModelState);
           // }
            return Ok(await _repos.AddEventsDetails(addEventsDto));
        }

        [HttpDelete("{eventid}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] string eventid)
        {
            return Ok(await _repos.DeleteEvent(eventid));
        }

        [HttpPut("{eventid}")]
        public async Task<IActionResult> UpdateEventDetails(string eventid, [FromBody] UpdateEventDto updateEventDto)
        {

            return Ok(await _repos.UpdateEventsDetails(eventid, updateEventDto));
        }
    }
}
