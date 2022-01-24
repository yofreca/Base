using Core.DTOs;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILoginService
    {
        Task<Login> GetLoginByCredentials(UserLoginDto login);
    }
}