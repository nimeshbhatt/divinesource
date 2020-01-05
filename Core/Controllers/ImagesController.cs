using Domain.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain;
using System.Linq;
using Service.Interface;
using Domain.Global;
using System.IO;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController: BaseApiController
    {
        private readonly IImageHandler _imageHandler;
        private readonly IImageWriter _imageWriter;

        public ImagesController(IImageHandler imageHandler, IImageWriter imageWriter)
        {
            _imageHandler = imageHandler;
            _imageWriter = imageWriter;
        }

        [HttpPost("multiple")]
        public async Task<List<IActionResult>> UploadImage(IFormFile file, string x, Guid UserId)
        {
            var a = Request.Form.Files;

            List<IActionResult> list = new List<IActionResult>();
            foreach (var item in a)
            {
                list.Add(_imageHandler.UploadImage(file, x, UserId).Result);
            }
            return list;
        }

        [HttpDelete("imagedelete/{id}")]
        public async Task<ObjectResult> DeleteImage(Guid id)
        {
            ObjectResult objectResult;

            try
            {
                var status = await _imageWriter.DeleteMedias(id);
                objectResult = status != null
                    ? GetObjectResponse(string.Empty, status) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse("Image Not Found", isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpGet("clientnewsimages")]
        public async Task<ObjectResult> ClientNewsImages()
        {
            ObjectResult objectResult;
            try
            {

                var userList = await _imageWriter.GetClientAppNewsImages();
                objectResult = userList != null && userList.Count > 0
                    ? GetObjectResponse(string.Empty, userList) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpGet("clientdocument/{id}")]
        public async Task<ObjectResult> ClientDocuments(Guid id)
        {
            ObjectResult objectResult;
            try
            {
                var userList = await _imageWriter.GetClientAppDocuments(id);
                objectResult = userList != null && userList.Count > 0
                    ? GetObjectResponse(string.Empty, userList) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpPost("download")]
        public async Task<IActionResult> Download(string filePath)
        {
            if (filePath == null)
                return Content("filename not present");

            var path = Path.Combine(filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        [HttpPost("store")]
        public async Task<ObjectResult> StoreMedia(List<UploadDocumentImage> medias)
        {
            ObjectResult objectResult;
            var mediaList = new List<UploadDocumentImageAc>();

            var mediasToCreate = new List<UploadDocumentImage>();

            try
            {
                if (mediasToCreate != null && mediasToCreate.Count > 0)
                {
                    mediasToCreate.ForEach(media =>
                    {
                        mediaList.Add(new UploadDocumentImageAc
                        {
                            DocumentPath = "./Uploads/" + media.DocumentPath,
                            Type = media.Type,
                            IsDelete = "0"
                        });
                    });
                    await _imageWriter.CreateMedias(mediaList);
                }
                objectResult = GetObjectResponse("Upload Successful.");
                return objectResult;
            }
            catch(Exception ex)
            {
                return GetObjectResponse("Upload Failed: " + ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
            throw new ExecutionEngineException();
        }
    }
}