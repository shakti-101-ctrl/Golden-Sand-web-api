using GoldenSand_WebAPI.DTOs.Admin.Income;
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
    public class IncomeController : ControllerBase
    {
        
        private readonly IAdminRespository _repos;
        public IncomeController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIncome()
        {
            return Ok(await _repos.GetAllIncome());
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeById(string id)
        {
            return Ok(await _repos.GetIncomeDetailsByIncomeId(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostIncome([FromBody] AddIncomeDetailsDto addNoticeDto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            return Ok(await _repos.AddIncomeDetails(addNoticeDto));
        }

        [HttpDelete("{incomeid}")]
        public async Task<IActionResult> DeleteIncomeDetails([FromRoute] string incomeid)
        {
            return Ok(await _repos.DeleteIncomeDetails(incomeid));
        }

        [HttpPut("{incomeid}")]
        public async Task<IActionResult> UpdateNoticeDetails(string incomeid, [FromBody] UpdateIncomeDetailsDto update)
        {

            return Ok(await _repos.UpdateIncomeDetails(incomeid, update));
        }
    }
}
