using DataAccess.EShop.Entities;
using DataAccess.EShop.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.IServices
{
    public interface IUserRepository
    {
        Task<User> Login(UserLoginRequestData requestData);
        Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData);
        Task<Function> GetFunctionByCode(string functionCode);
        Task<UserFunction> UserFunction_GetRole(int userID, int functionID);
    }
}
