using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Models;

namespace Service.Interface
{
    public interface IServiceEngineer  
    {
        Task<ServiceEngineerAc> Create(ServiceEngineerAc serviceEngineer);
        Task<List<ServiceEngineerAc>> ListServiceEngineer();
        Task<ServiceEngineerAc> GetById(string id);
        Task<ServiceEngineerAc> Update(ServiceEngineerAc serviceEngineer);
        Task<ServiceEngineerAc> Delete(string id);
    }
}
