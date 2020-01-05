using Domain;
using Domain.Models;
using Domain.Context;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly DivineContext _context;
        private readonly IMapper _mapper;

        public UserService(DivineContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersAc> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var changePassword = await _context
            .Users.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString() && x.Password == oldPassword);
            changePassword.Password = newPassword;
            await _context.SaveChangesAsync();

            return _mapper.Map<UsersAc>(changePassword);
        }

        public async Task<UsersAc> Create(UsersAc users)
        {
            var userToCreate = new Users();
            userToCreate.Id = Guid.NewGuid();
            userToCreate.FirstName = users.FirstName;
            userToCreate.LastName = users.LastName;
            userToCreate.Email = users.Email;
            userToCreate.Phone = users.Phone;
            userToCreate.CompanyName = users.CompanyName;
            userToCreate.CompanyAddress = users.CompanyAddress;
            userToCreate.CompanyPhone = users.CompanyPhone;
            userToCreate.CompanyWebSite = users.CompanyWebSite;
            userToCreate.Password = users.Password;
            userToCreate.Role = "1";
            userToCreate.IsDelete = "0";
            _context.Users.Add(userToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsersAc>(users);
        }

        public async Task<UsersAc> Delete(string id)
        {
            var userToDelete = await _context.Users.Where(x => id.ToString().Contains(x.Id.ToString())).ToListAsync();
            if (userToDelete != null && userToDelete.Count > 0)
            {
                _context.Users.RemoveRange(userToDelete);
                await _context.SaveChangesAsync();
            }
            var count = userToDelete.Count;
            return _mapper.Map<UsersAc>(userToDelete);
        }

        public async Task<UsersAc> GetById(string id)
        {
            var result = await _context
            .Users.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());

            return _mapper.Map<UsersAc>(result);
        }

        public async Task<List<UsersAc>> ListAsync()
        {
            var result = await _context
            .Users
            .ToListAsync();
            return _mapper.Map<List<UsersAc>>(result);
        }

        public async Task<UsersAc> Login(Users user)
        {
            if (user.Role == "0")
            {
                var userToLogin = await _context.Users.FirstOrDefaultAsync(x => x.Password == user.Password && x.Email == user.Email && x.Role == "0" && x.IsDelete == "0");
                return _mapper.Map<UsersAc>(userToLogin);
            }
            else
            {
                var userToLogin = await _context.Users.FirstOrDefaultAsync(x => x.Password == user.Password && x.Email == user.Email && x.Role == "1" && x.IsDelete == "0");
                return _mapper.Map<UsersAc>(userToLogin);
            }
        }

        public async Task<UsersAc> Update(UsersAc usersAc)
        {
            var updateUser = await _context
            .Users.FirstOrDefaultAsync(x => x.Id.ToString() == usersAc.Id.ToString());
            updateUser.FirstName = usersAc.FirstName;
            updateUser.LastName = usersAc.LastName;
            updateUser.Email = usersAc.Email;
            updateUser.Phone = usersAc.Phone;
            updateUser.CompanyName = usersAc.CompanyName;
            updateUser.CompanyAddress = usersAc.CompanyAddress;
            updateUser.CompanyPhone = usersAc.CompanyPhone;
            updateUser.CompanyWebSite = usersAc.CompanyWebSite;
            _context.Users.Add(updateUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<UsersAc>(updateUser);
        }
    }
}