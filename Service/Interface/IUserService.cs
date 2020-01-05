using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Models;


namespace Service.Interface
{
    public interface IUserService
    {        
        Task<UsersAc> Create(UsersAc users);
        Task<List<UsersAc>> ListAsync();
        Task<UsersAc> GetById(string id);
        Task<UsersAc> Login(Users user);
        Task<UsersAc> Update(UsersAc usersAc);
        Task<UsersAc> Delete(string id);
        Task<UsersAc> ChangePassword(Guid id, string oldPassword, string newPassword);
    }
}
