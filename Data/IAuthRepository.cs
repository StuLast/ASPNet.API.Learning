using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.rpg.Data
{
    public interface IAuthRepository
    {
        public Task<ServiceResponse<int>> Register (User newUser, string newPassword);
        public Task<ServiceResponse<string>> Login (string username, string password);
        public Task<bool> userExists (string username);
    }
}