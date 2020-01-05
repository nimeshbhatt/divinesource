using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//using CSFileIOWithDBASPNETCore.Models;
using System.IO;
using System.Net.Http;
using System.Web.Http.Results;
using System.Drawing;
using Domain;
using Domain.Models;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                var result = new List<UploadDocumentImage>();
                foreach (var file in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    result.Add(new UploadDocumentImage() {DocumentPath = file.FileName, Type = "1", IsDelete = "0" });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}