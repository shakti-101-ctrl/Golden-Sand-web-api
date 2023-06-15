using GoldenSand_WebAPI.DTOs.Admin.Expense;
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
    public class ExpenseController : ControllerBase
    {
        ServiceResponse<IActionResult> service = new ServiceResponse<IActionResult>();
        private readonly IAdminRespository _repos;
        public ExpenseController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _repos.GetAllExpenses());
        }

        // GET: api/Brand/5
        [HttpGet("{expenseid}")]
        public async Task<IActionResult> GetExpenseByExpenseId(string expenseid)
        {
            return Ok(await _repos.GetExpenseByExpenseId(expenseid));
        }
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] AddExpenseDetailsDto addExpenseDetailsDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return UnprocessableEntity(ModelState);
            //}
            addExpenseDetailsDto.EntryDate = DateTime.Now;
            return Ok(await _repos.AddExpenseDetails(addExpenseDetailsDto));
        }

        [HttpDelete("{noticeid}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] string noticeid)
        {
            return Ok(await _repos.DeleteExpense(noticeid));
        }

        [HttpPut("{noticeid}")]
        public async Task<IActionResult> UpdateExpenseDetails(string noticeid, [FromBody] UpdateExpenseDetailsDto update)
        {

            return Ok(await _repos.UpdateExpenseDetails(noticeid, update));
        }
    }
}
