using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain;
using Domain.Models;
using Domain.Enum;

namespace Domain.Handler
{
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file, string x, Guid medias);
        Task<IActionResult> AddServiceRequest(IFormFileCollection file, ServiceTypeEnum x, Guid id, string title, string description);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        private readonly IServiceQuotationService _serviceQuotationService;

        public ImageHandler(IImageWriter imageWriter, IServiceQuotationService serviceQuotationService)
        {
            _imageWriter = imageWriter;
            _serviceQuotationService = serviceQuotationService;
        }

        public async Task<IActionResult> AddServiceRequest(IFormFileCollection file, ServiceTypeEnum x, Guid id, string title, string description)
        {
            var result = await _serviceQuotationService.AddServiceRequest(file, x, id, title, description);
            return new ObjectResult(result);
        }

        public async Task<IActionResult> UploadImage(IFormFile file, string x, Guid medias)
        {
            var result = await _imageWriter.UploadImage(file, x, medias);
            return new ObjectResult(result);
        }
    }
}
