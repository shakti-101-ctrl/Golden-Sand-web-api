using GoldenSand_WebAPI.DTOs.Admin.Employee;
using GoldenSand_WebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GoldenSand_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        private readonly IAdminRespository _repos;
        public EmployeeController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _repos.GetAllEmployees());
        }

        // GET: api/Brand/5
        [HttpGet("{empid}")]
        public async Task<IActionResult> GetEmployeeByEmpId(string empid)
        {
            return Ok(await _repos.GetEmployeeDetails(empid));
        }
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromForm] AddEmployeeDetailsDto addEmployeeDetailsDto)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();


                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                //for adhar card
                if (formCollection.Files.Count > 0)
                {
                   
                    //logic using loop
                    for (int i = 0; i < formCollection.Files.Count; i++)
                    {
                        var file = formCollection.Files[i];
                        var fieldName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                        if (fieldName == "ScanCopyOfAdharCard")
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            addEmployeeDetailsDto.ScanCopyOfAdharCard = await UploadPhotos(file, fileName);
                        }
                        else if (fieldName == "Photo")
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            addEmployeeDetailsDto.Photo = await UploadPhotos(file, fileName);
                        }
                    }

                }
                else
                {
                    return BadRequest();
                }
                addEmployeeDetailsDto.EntryDate = DateTime.Now;
                return Ok(await _repos.AddEmployeeDetails(addEmployeeDetailsDto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpDelete("{empid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] string empid)
        {
            return Ok(await _repos.DeleteEmployeeDetails(empid));
        }

        [HttpPut("{empid}")]
        public async Task<IActionResult> UpdateEmployee(string empid, [FromForm] UpdateEmployeeDetailsDto updateEmployeeDetailsDto)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                //logic using loop
                for (int i = 0; i < formCollection.Files.Count; i++)
                {
                    var file = formCollection.Files[i];
                    var fieldName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"');
                    if (fieldName == "ScanCopyOfAdharCard")
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        updateEmployeeDetailsDto.ScanCopyOfAdharCard = await UploadPhotos(file, fileName);
                    }
                    else if (fieldName == "Photo")
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        updateEmployeeDetailsDto.Photo = await UploadPhotos(file, fileName);
                    }
                }
                //end logic

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            return Ok(await _repos.UpdateEmployeeDetails(empid, updateEmployeeDetailsDto));
        }
        private async Task<string> UploadPhotos(IFormFile file, object fileName)
        {

            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var newfileName = DateTime.Now.ToString("yymmssfff") + fileName;
            var fullPath = Path.Combine(pathToSave, newfileName);
            var dbPath = Path.Combine(folderName, newfileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return dbPath.Replace("\\", "/");
        }
    }
}
