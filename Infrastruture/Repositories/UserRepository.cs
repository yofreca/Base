using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly BaseContext _context;
        public UserRepository(BaseContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(long id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.UserId == id);
            return user;
        }

        public async Task InsertUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async  Task<bool> UpdateUser(User user)
        {
            var currentUser = await GetUser(user.UserId);
            currentUser.FirstName = user.FirstName;
            currentUser.Email = user.Email;

            int rows = await  _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteUser(long id)
        {
            var currentUser = await GetUser(id);
            _context.User.Remove(currentUser);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }



    }
}
