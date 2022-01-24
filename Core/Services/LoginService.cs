using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Login> GetLoginByCredentials(UserLoginDto login)
        {
            return await _loginRepository.GetLoginByCredentials(login);
        }

    }
}
