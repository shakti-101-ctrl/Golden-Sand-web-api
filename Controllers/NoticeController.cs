using GoldenSand_WebAPI.DTOs.Admin.Notice;
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
    
    public class NoticeController : ControllerBase
    {
        ServiceResponse<IActionResult> service = new ServiceResponse<IActionResult>();
        private readonly IAdminRespository _repos;
        public NoticeController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNotice()
        {
            return Ok(await _repos.GetAllNotice());
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticeById(string id)
        {
            return Ok(await _repos.GetNoticeById(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostNotice([FromBody] AddNoticeDto addNoticeDto)
        {
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            return Ok(await _repos.AddNoticeDetails(addNoticeDto));
        }

        [HttpDelete("{noticeid}")]
        public async Task<IActionResult> DeleteNotice([FromRoute] string noticeid)
        {
            return Ok(await _repos.DeleteNotice(noticeid));
        }

        [HttpPut("{noticeid}")]
        public async Task<IActionResult> UpdateNoticeDetails(string noticeid,[FromBody]UpdateNoticeDto update)
        {

            return Ok(await _repos.UpdateNoticeDetails(noticeid,update));
        }
    }
}
