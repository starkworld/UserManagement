using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Data;

namespace UserManagement.Api.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<Users>> GetUsersList();
        Task CreateUserAsync(Users user);
        Task<IEnumerable<Users>> FindUsersByUsername(string username);
        Task<Users> FindUserById(int Id);
    }
}
