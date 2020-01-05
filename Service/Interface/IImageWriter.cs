using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interface
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file, string x, Guid medias);
        Task CreateMedias(List<UploadDocumentImageAc> medias);
        Task<UploadDocumentImageAc> DeleteMedias(Guid id);
        Task<List<string>> GetClientAppNewsImages();
        Task<List<string>> GetClientAppDocuments(Guid UserId);
    }
}
