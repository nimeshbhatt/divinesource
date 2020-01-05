using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Service.Interface;
using Domain.Context;
using AutoMapper;
using Domain;

namespace Service.Services
{
    public class UploadDocumentImageService : IUploadDocumentImageService
    {
        private readonly DivineContext _context;
        private readonly IMapper _mapper;

        public UploadDocumentImageService(DivineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateMedias(List<UploadDocumentImageAc> medias)
        {
            medias.ForEach(x =>
            {
                var media = new UploadDocumentImage();
                media.DocumentPath = x.DocumentPath;
                media.Type = x.Type;
                media.IsDelete = "0";
                _context.UploadDocumentImages.Add(media);
            });

            await _context.SaveChangesAsync();
        }

        public Task DeleteMedias(List<ulong> ids)
        {
            throw new NotImplementedException();
        }
    }
}
