using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Interface
{
    public interface IUploadDocumentImageService
    {
        Task CreateMedias(List<UploadDocumentImageAc> medias);
        Task DeleteMedias(List<ulong> ids);
    }
}
