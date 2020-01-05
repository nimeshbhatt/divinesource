using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ImageController
    {
        private readonly IHostingEnvironment _environment;
        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        // POST: api/Image
        [HttpPost("upload")]
        public async Task Post(IFormFile file)
        {
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        
    }
}
