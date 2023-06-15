using GoldenSand_WebAPI.DTOs.Admin.Duplex;
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
    public class DuplexController : ControllerBase
    {
        ServiceResponse<IActionResult> service = new ServiceResponse<IActionResult>();
        private readonly IAdminRespository _repos;
        public DuplexController(IAdminRespository repos)
        {
            _repos = repos;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDuplexDetails()
        {
            return Ok(await _repos.GetAllDuplex());
        }

        // GET: api/Brand/5
        [HttpGet("{duplexid}")]
        public async Task<IActionResult> GetNoticeById(string duplexid)
        {
            return Ok(await _repos.GetDuplexDetailsById(duplexid));
        }
        [HttpPost]
        public async Task<IActionResult> SaveDuplexDetails([FromForm]AddDuplexDetailsDto addDuplexDetails)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
               

                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                //for adhar card
                if (formCollection.Files.Count>0)
                {
                    var file = formCollection.Files[0];
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var newfileName = DateTime.Now.ToString("yymmssfff") + fileName;
                    var fullPath = Path.Combine(pathToSave, newfileName);
                    var dbPath = Path.Combine(folderName, newfileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    addDuplexDetails.AdharCardCopy = dbPath.Replace("\\", "/");

                    //image2
                    var file1 = formCollection.Files[1];
                    
                        var fileName1 = ContentDispositionHeaderValue.Parse(file1.ContentDisposition).FileName.Trim('"');
                        var newfileName1 = DateTime.Now.ToString("yymmssfff") + fileName1;
                        var fullPath1 = Path.Combine(pathToSave, newfileName1);
                        var dbPath1 = Path.Combine(folderName, newfileName1);
                        using (var stream = new FileStream(fullPath1, FileMode.Create))
                        {
                            file1.CopyTo(stream);
                        }
                        addDuplexDetails.PhotoCopy = dbPath1.Replace("\\", "/");
                
                }
                else
                {
                    return BadRequest();
                }
                addDuplexDetails.EntryDate = DateTime.Now;
                return Ok(await _repos.AddDuplexDetails(addDuplexDetails));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            
        }

        [HttpDelete("{duplexid}")]
        public async Task<IActionResult> DeleteDuplexDetails([FromRoute] string duplexid)
        {
            return Ok(await _repos.DeleteDuplexDetails(duplexid));
        }

        [HttpPut("{duplexid}")]
        public async Task<IActionResult> UpdateNoticeDetails(string duplexid, [FromForm] UpdateDuplexDetailsDto updateDuplex)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                //logic using loop
                for(int i=0;i<formCollection.Files.Count;i++)
                {
                    var file = formCollection.Files[i];
                    var fieldName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).Name.Trim('"') ;
                    if(fieldName=="AdharCardCopy")
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        updateDuplex.AdharCardCopy = await UploadPhotos(file, fileName);
                    }
                    else if(fieldName=="PhotoCopy")
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        updateDuplex.PhotoCopy = await UploadPhotos(file, fileName);
                    }
                }
                //end logic
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            return Ok(await _repos.UpdateDuplexDetails(duplexid, updateDuplex));
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
            return dbPath.Replace("\\","/");
        }
    }
}
