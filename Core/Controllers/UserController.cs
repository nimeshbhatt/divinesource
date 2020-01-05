using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
using Service.Services;
using Domain.Global;
using Domain.Models;
using Domain;
using Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly DivineContext _context;

        public UserController(DivineContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("changepassword")]
        public async Task<ObjectResult> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            ObjectResult objectResult;
            try
            {
                var changePassword = await _context
            .Users.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString() && x.Password == oldPassword);
                if (changePassword == null)
                {
                    return GetObjectResponse("Old password not matched", isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
                }
                var userAc = await _userService.ChangePassword(id, oldPassword, newPassword);
                objectResult = userAc != null ? GetObjectResponse(string.Empty, userAc)
                    : GetObjectResponse(
                        Constants.UserNameAlreadyExists, statusCode: System.Net.HttpStatusCode.BadRequest, isSuccess: false);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
            return objectResult;
        }

        [HttpGet("userlist")]
        public async Task<ObjectResult> ListUserAsync()
        {
            ObjectResult objectResult;
            try
            {

                var userList = await _userService.ListAsync();
                objectResult = userList != null && userList.Count > 0
                    ? GetObjectResponse(string.Empty, userList) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpPost("create")]
        public async Task<ObjectResult> Create([FromBody]UsersAc user)
        {
            ObjectResult objectResult;
            try
            {
                var userAc = await _userService.Create(user);
                objectResult = userAc != null ? GetObjectResponse(string.Empty, userAc)
                    : GetObjectResponse(
                        Constants.UserNameAlreadyExists, statusCode: System.Net.HttpStatusCode.BadRequest, isSuccess: false);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
            return objectResult;
        }

        [HttpPost("login")]
        public async Task<ObjectResult> Login([FromBody]Users user)
        {
            ObjectResult objectResult;
            try
            {
                var userAc = await _userService.Login(user);
                objectResult = userAc != null ? GetObjectResponse(string.Empty, userAc)
                    : GetObjectResponse(Constants.UnAuthorized, statusCode: System.Net.HttpStatusCode.Unauthorized, isSuccess: false);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
            return objectResult;
        }

        [HttpPut("userupdate")]
        public async Task<ObjectResult> Update([FromBody]UsersAc usersAc)
        {
            ObjectResult objectResult;
            try
            {
                var updateEng = await _userService.Update(usersAc);
                objectResult = GetObjectResponse(string.Empty, updateEng);
            }
            catch (Exception ex)
            {
                return GetObjectResponse(ex.Message, isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }

        [HttpDelete("userdelete/{id}")]
        public async Task<ObjectResult> Delete(string id)
        {
            ObjectResult objectResult;

            try
            {
                var status = await _userService.Delete(id);
                objectResult = status != null
                    ? GetObjectResponse(string.Empty, status) : GetObjectResponse(Constants.NoRecordFound);
            }
            catch (Exception ex)
            {
                return GetObjectResponse("User Not Found", isSuccess: false, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

            return objectResult;
        }
    }
}
