using GoldenSand_WebAPI.DTOs.Admin.Tenant;
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
    public class TenantController : ControllerBase
    {
        
        private readonly IAdminRespository _repos;
        public TenantController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            return Ok(await _repos.GetAllTenant());
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantByTenantId(string id)
        {
            return Ok(await _repos.GetTenantDetailsByTenantId(id));
        }
        [HttpPost]
        public async Task<IActionResult> PostTenantDetails([FromBody] AddTenantDetailsDto addTenantDetailsDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return UnprocessableEntity(ModelState);
            //}
            return Ok(await _repos.AddTenantDetails(addTenantDetailsDto));
        }

        [HttpDelete("{tenantid}")]
        public async Task<IActionResult> DeleteTenant([FromRoute] string tenantid)
        {
            return Ok(await _repos.DeleteTenantDetails(tenantid));
        }

        [HttpPut("{tenantid}")]
        public async Task<IActionResult> UpdateNoticeDetails(string tenantid, [FromBody] UpdateTenantDetailsDto updateTenant)
        {

            return Ok(await _repos.UpdateTenantDetails(tenantid, updateTenant));
        }
    }
}
