using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using UserManagement.Api.Data;
using UserManagement.Api.Providers;
using UserManagement.Api.Repositories;
using Xunit;

namespace UserManagement.UnitTest
{
    public class UsersUnitTest
    {
        //Memory Database Context
        private async Task<UMDBContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<UMDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new UMDBContext(options);
            databaseContext.Database.EnsureCreated();

            //Create some users
            if (await databaseContext.Users.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Users.Add(new Users()
                    {
                        Id = i,
                        FirstName = "Test",
                        LastName = "User",
                        Age = 27,
                        Address = "United Kingdom",
                        Interests = "Asp.net core Programming"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        [Fact]
        public async Task GetUsersListTest()
        {
            //Setup DbContext mock  
            var _context = await GetDatabaseContext();
            var _users = new UsersProvider(_context);
            //Assert  
            Assert.NotNull(_users.GetUsersList());
        }


        [Fact]
        public async Task CreateUserAsyncTest()
        {
            //Setup DbContext mock  
            var _context = await GetDatabaseContext();
            var _users = new UsersProvider(_context);
            //Assert  
            try
            {
                await _users.CreateUserAsync(new Users
                {
                    FirstName = "Test",
                    LastName = "User2",
                    Age = 27,
                    Address = "United Kingdom",
                    Interests = "Asp.net core and C# Programming"
                });

            }
            catch (Exception ex)
            {
                throw new Exception("Cannot add a new record.");
            }

        }



        [Fact]
        public async Task FindUsersByUsernameTest()
        {
            //Setup DbContext mock  
            var _context = await GetDatabaseContext();
            var _users = new UsersProvider(_context);
            //Assert  
            Assert.NotNull(_users.FindUsersByUsername("test"));
        }

        [Fact]
        public async Task FindUsersByIdTest()
        {
            //Setup DbContext mock  
            var _context = await GetDatabaseContext();
            var _users = new UsersProvider(_context);
            //Assert  
            Assert.NotNull(_users.FindUserById(1));
        }
    }
}
