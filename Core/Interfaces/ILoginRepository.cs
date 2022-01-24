using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login> GetLoginByCredentials(UserLoginDto login);
    }
}