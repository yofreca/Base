using System.Threading.Tasks;
using Core.CustomEntities;
using Core.Entities;
using Core.Interfaces;
using Core.QueryFilters;
using Microsoft.Extensions.Options;

namespace Core.Services
{

    public class UserService : IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly PaginationOptions _paginationOptions;
        public UserService(IUserRepository userRepository, IOptions<PaginationOptions> options)
        {
            _userRepository = userRepository;
            _paginationOptions = options.Value;
        }

        public async Task<bool> DeleteUser(long id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<User> GetUser(long id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<PagedList<User>> GetUsers(UserQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber: filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var users = await _userRepository.GetUsers();
            var pagedUsers = PagedList<User>.Create(users, filters.PageNumber, filters.PageSize);

            return pagedUsers;
        }

        public async Task InsertUser(User user)
        {
            await _userRepository.InsertUser(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user);
        }
    }
}
