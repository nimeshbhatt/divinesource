using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IApiService
    {
        Task<bool> UploadImageAsync(Stream image, string fileName);
    }
}
