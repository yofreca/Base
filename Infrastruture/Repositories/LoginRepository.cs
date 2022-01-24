using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Repositories
{
 
    public class LoginRepository : ILoginRepository
    {
        private readonly BaseContext _context;
        public LoginRepository(BaseContext context)
        {
            _context = context;
        }


        public async Task<Login> GetLoginByCredentials(UserLoginDto login)
        {
            return await _context.Login.FirstOrDefaultAsync(x =>
                x.UserName == login.User && x.Password == login.Password);
        }

    }
}
