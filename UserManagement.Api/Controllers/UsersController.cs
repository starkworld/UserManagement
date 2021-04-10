using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Data;
using UserManagement.Api.Repositories;

namespace UserManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("UsersPolicy")]
    public class UsersController : Controller
    {
        private IUsersRepository _user { get; set; }
        public UsersController(IUsersRepository user)
        {
            _user = user;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Users>> GetUsers()
        {
            return await _user.GetUsersList();
        }
        [HttpPost]
        [Route("Post")]
        public async Task<JsonResult> CreateUsers([Bind("Id,FirstName,LastName,Address,Age,Interests")] Users user)
        {
            try
            {
                await _user.CreateUserAsync(user);
                return new JsonResult(new { key = true, value = user, msg = "User Created Successfully." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { key = false, value = user, msg = "Unable to process your request. Please contact to your admin." });
            }
        }

        [HttpGet("Search/{username}")]
        public async Task<IEnumerable<Users>> FindUser(string username)
        {
            return await _user.FindUsersByUsername(username);
        }

        [HttpGet("Find/{Id}")]
        public async Task<Users> GetUser(int Id)
        {
            return await _user.FindUserById(Id);
        }

    }
}
