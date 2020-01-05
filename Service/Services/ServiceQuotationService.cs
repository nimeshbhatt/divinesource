using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.Interface;
using Domain;
using Domain.Models;
using Domain.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Domain.Enum;
using System.Net.Mail;
using System.Net;

namespace Service.Services
{
    public class ServiceQuotationService : IServiceQuotationService
    {
        private readonly DivineContext _context;
        private readonly IMapper _mapper;

        public ServiceQuotationService(DivineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<string> AddQuotationRequest(string x, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> AddServiceRequest(IFormFileCollection file, ServiceTypeEnum x, Guid id, string title, string description)
        {
            return await NewServiceQuotation(file, x, id, title, description);
        }

        public async Task<ServiceQuotation> GetServiceQuotationById(Guid id)
        {
            var a = await _context.ServiceQuotations
                .Include(y => y.ServiceAttachments)
                .Include(y => y.Users)
                .Include(y => y.ServiceEngineer)
                .FirstOrDefaultAsync(y => y.Id == id);
            return a;
        }

        public async Task<List<string>> NewServiceQuotation(IFormFileCollection file, ServiceTypeEnum x, Guid id, string title, string description)
        {
            var details = new ServiceQuotation();
            details.Id = Guid.NewGuid();
            details.UserId = id;
            details.IsReply = "0";
            if(x == 0)
            {
                details.ServiceTitle = title;
                details.ServiceDescription = description;
                details.QuotationTitle = "";
                details.QuotationDesc = "";
            }
            else
            {
                details.ServiceTitle = "";
                details.ServiceDescription = "";
                details.QuotationTitle = title;
                details.QuotationDesc = description;
            }
            details.Type = x;
            _context.ServiceQuotations.Add(details);
            await _context.SaveChangesAsync();

            List<string> fileNamesList = new List<string>();
            List<ServiceAttachment> serviceAttachmentsList = new List<ServiceAttachment>();
            foreach (IFormFile singleFile in file)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", singleFile.FileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await singleFile.CopyToAsync(bits);
                    fileNamesList.Add(singleFile.FileName);

                    // Save file details to the database
                    serviceAttachmentsList.Add(new ServiceAttachment
                    {
                        ServiceId = details.Id,
                        ServiceAttachPath = path
                    });
                }
            }

            _context.ServiceAttachments.AddRange(serviceAttachmentsList);
            await _context.SaveChangesAsync();

            SendEmail();
            return fileNamesList;
        }

        public async Task SendEmail()
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp-mail.outlook.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("nimesh.cb@outlook.com", "codeblaze123");
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nimesh.cb@outlook.com");
            mailMessage.To.Add("nimeshbhatt1005@gmail.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }


        public async Task<List<ServiceQuotation>> ServiceQuotationList(ServiceTypeEnum x)
        {
            var result = await _context
            .ServiceQuotations.Where(y => y.Type == x).ToListAsync();
            return _mapper.Map<List<ServiceQuotation>>(result);
        }

        public async Task<ServiceQuotation> ReplyToService(Guid serviceId, Guid serviceEngId, DateTime dateTime)
        {
            var updateReply = await _context.ServiceQuotations
                .FirstOrDefaultAsync(x => x.Id == serviceId);
            updateReply.ServiceEngId = serviceEngId;
            updateReply.ServiceDate = dateTime;
            updateReply.IsReply = "1";
            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceQuotation>(updateReply);

        }
    }
}
