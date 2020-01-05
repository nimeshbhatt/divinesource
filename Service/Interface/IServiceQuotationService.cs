using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Models;

namespace Service.Interface
{
    public interface IServiceQuotationService
    {
        Task<List<string>> AddServiceRequest(IFormFileCollection file, ServiceTypeEnum x, Guid id, string title, string description);
        Task<string> AddQuotationRequest(string x, Guid id);
        Task<ServiceQuotation> GetServiceQuotationById(Guid id);
        Task<List<ServiceQuotation>> ServiceQuotationList(ServiceTypeEnum x);
        Task<ServiceQuotation> ReplyToService(Guid userId, Guid serviceEngId, DateTime dateTime);
    }
}
