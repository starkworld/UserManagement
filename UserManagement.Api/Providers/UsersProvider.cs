using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Data;
using UserManagement.Api.Repositories;

namespace UserManagement.Api.Providers
{
    public class UsersProvider : IUsersRepository
    {
        private UMDBContext _context { get; set; }
        public UsersProvider(UMDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Users>> GetUsersList()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task CreateUserAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Users>> FindUsersByUsername(string username)
        {
            return await _context.Users.Where(x => x.FirstName.Contains(username) || x.LastName.Contains(username)).ToListAsync();
        }
        public async Task<Users> FindUserById(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }
    }
}
