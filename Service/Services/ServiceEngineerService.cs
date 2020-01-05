using Domain;
using Domain.Models;
using Domain.Context;
using Domain.Global;
using Service;
using Service.Interface;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Service.Services
{
    public class ServiceEngineerService : IServiceEngineer
    {
        private readonly DivineContext _context;
        private readonly IMapper _mapper;

        public ServiceEngineerService(DivineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceEngineerAc> Create(ServiceEngineerAc serviceEngineer)
        {
            var engToCreate = new ServiceEngineer();
            engToCreate.Id = Guid.NewGuid();
            engToCreate.FirstName = serviceEngineer.FirstName;
            engToCreate.LastName = serviceEngineer.LastName;
            engToCreate.Email = serviceEngineer.Email;
            engToCreate.Phone = serviceEngineer.Phone;
            engToCreate.Address = serviceEngineer.Address;
            engToCreate.DateOfJoin = serviceEngineer.DateOfJoin;
            engToCreate.IsDelete = "0";
            _context.ServiceEngineers.Add(engToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceEngineerAc>(serviceEngineer);
        }

        public async Task<ServiceEngineerAc> Delete(string id)
        {
            var serviceEngineerToDelete = await _context.ServiceEngineers.Where(x => id.ToString().Contains(x.Id.ToString())).ToListAsync();
            if(serviceEngineerToDelete != null && serviceEngineerToDelete.Count > 0)
            {
                _context.ServiceEngineers.RemoveRange(serviceEngineerToDelete);
                await _context.SaveChangesAsync();
            }
            var count = serviceEngineerToDelete.Count;
            return _mapper.Map<ServiceEngineerAc>(serviceEngineerToDelete);
        }

        public async Task<ServiceEngineerAc> GetById(string id)
        {
            var result = await _context
            .ServiceEngineers.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());

            return _mapper.Map<ServiceEngineerAc>(result);
        }

        public async Task<List<ServiceEngineerAc>> ListServiceEngineer()
        {
            var result = await _context
            .ServiceEngineers.ToListAsync();
            return _mapper.Map<List<ServiceEngineerAc>>(result);
        }

        public async Task<ServiceEngineerAc> Update(ServiceEngineerAc serviceEngineer)
        {
            var updateEng = await _context
            .ServiceEngineers.FirstOrDefaultAsync(x => x.Id.ToString() == serviceEngineer.Id.ToString());
            updateEng.FirstName = serviceEngineer.FirstName;
            updateEng.LastName = serviceEngineer.LastName;
            updateEng.Email = serviceEngineer.Email;
            updateEng.Phone = serviceEngineer.Phone;
            updateEng.Address = serviceEngineer.Address;
            updateEng.DateOfJoin = serviceEngineer.DateOfJoin;
            updateEng.IsDelete = serviceEngineer.IsDelete;
            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceEngineerAc>(updateEng);
        }
    }
}
