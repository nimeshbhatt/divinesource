using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain;
using Domain.Models;
using System.Linq;
using Domain.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace Service.Services
{
    public class ImageWriter : IImageWriter
    {
        private readonly DivineContext _context;
        private readonly IMapper _mapper;

        public ImageWriter(DivineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> UploadImage(IFormFile file, string x, Guid medias)
        {
            //if (CheckIfImageFile(file))
            //{
            return await WriteFile(file, x, medias);
            //}

            //return "Invalid image file";
        }

       

        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        public async Task<string> WriteFile(IFormFile file, string x, Guid medias)
        {
            //string fileName = "default";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //if (x == "0")
                //{
                //    fileName = fileName + "1" + extension;
                //}
                //else if (x == "1")
                //{
                //    fileName = fileName + "2" + extension;
                //}
                //else if (x == "2")
                //{
                //    fileName = fileName + "3" + extension;
                //}
                //else if (x == "3")
                //{
                //    fileName = fileName + "4" + extension;
                //}
                //else
                //{
                //    fileName = file.FileName + extension;
                //}

                //fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                //for the file due to security reasons.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", file.FileName + extension);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                var media = new UploadDocumentImage();
                media.Id = Guid.NewGuid();
                media.UserId = medias;
                media.DocumentPath = path;
                media.Type = x;
                media.IsDelete = "0";
                _context.UploadDocumentImages.Add(media);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            //return fileName;
            return file.FileName;
        }

        public async Task CreateMedias(List<UploadDocumentImageAc> medias)
        {
            medias.ForEach(x =>
            {
                var media = new UploadDocumentImage();
                media.Id = Guid.NewGuid();
                media.DocumentPath = x.DocumentPath;
                media.Type = x.Type;
                media.IsDelete = "0";
                _context.UploadDocumentImages.Add(media);
            });
            await _context.SaveChangesAsync();
        }

        public async Task<UploadDocumentImageAc> DeleteMedias(Guid id)
        {
            var imageToDelete = await _context.UploadDocumentImages.Where(x => id.ToString().Contains(x.Id.ToString())).ToListAsync();
            if (imageToDelete != null && imageToDelete.Count > 0)
            {
                _context.UploadDocumentImages.RemoveRange(imageToDelete);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<UploadDocumentImageAc>(imageToDelete);
        }

        public async Task<List<string>> GetClientAppNewsImages()
        {
            return (await _context.UploadDocumentImages.Where(x => x.Type == "0").ToListAsync()).Select(x => x.DocumentPath).ToList();
        }

        public async Task<List<string>> GetClientAppDocuments(Guid UserId)
        {
            return (await _context.UploadDocumentImages.Where(x => x.Type == "1" && x.UserId == UserId).ToListAsync()).Select(x => x.DocumentPath).ToList();
        }
    }
}
