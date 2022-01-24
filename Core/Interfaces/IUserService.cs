using System.Collections.Generic;
using Core.Entities;
using System.Threading.Tasks;
using Core.CustomEntities;
using Core.QueryFilters;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<User>> GetUsers(UserQueryFilter filters);
        Task<User> GetUser(long id);
        Task InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(long id);
    }
}