using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Service.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace Core.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class UploadDocumentImageController : BaseApiController
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IUploadDocumentImageService _mediaService;

        public UploadDocumentImageController(IHostingEnvironment hostingEnvironment, IUploadDocumentImageService mediaService)
        {
            _hostingEnvironment = hostingEnvironment;
            _mediaService = mediaService;
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ObjectResult> UploadFile()
        {
            ObjectResult objectResult;
            var mediaList = new List<UploadDocumentImageAc>();
            try
            {
                //Request.Form.Files.ToList().ForEach(file =>
                //{
                //    string folderName = "Uploads";
                //    string webRootPath = _hostingEnvironment.WebRootPath;
                //    string newPath = Path.Combine(webRootPath, folderName);
                //    if (!Directory.Exists(newPath))
                //    {
                //        Directory.CreateDirectory(newPath);
                //    }
                //    if (file.Length > 0)
                //    {
                //        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                //        string fullPath = Path.Combine(newPath, fileName);
                //        using (var stream = new FileStream(fullPath, FileMode.Create))
                //        {
                //            file.CopyTo(stream);
                //        }
                //    }
                //});

                //if (medias.Count > 0)
                //{
                //    await _mediaService.CreateMedias(medias);
                //}
                objectResult = GetObjectResponse("Upload Successful.");
                return objectResult;
            }
            catch (System.Exception ex)
            {
                return GetObjectResponse("Upload Failed: " + ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
        }

        //public async Task<ObjectResult> StoreMedia([System.Web.Http.FromBody]List<UploadDocumentImageAc> medias)
        //{

        //}
    }
}
