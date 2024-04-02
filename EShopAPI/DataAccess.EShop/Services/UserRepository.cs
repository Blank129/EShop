using DataAccess.EShop.Entities;
using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using DataAccess.EShop.RequestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Services
{
    public class UserRepository : IUserRepository
    {
        private EShopDBContext DbContext;
        public UserRepository(EShopDBContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData)
        {
            try
            {
                var user = DbContext.User.Where(s => s.UserId == requestData.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.RefreshToken = requestData.RefeshToken;
                    user.TokenExpriedDate = requestData.RefeshTokenExpired;
                    DbContext.User.Update(user);
                    DbContext.SaveChanges();

                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Function> GetFunctionByCode(string functionCode)
        {
            return DbContext.Function.Where(s => s.FunctionCode == functionCode).FirstOrDefault();
        }

        public async Task<User> Login(UserLoginRequestData requestData)
        {
            try
            {
                var user = DbContext.User.Where(s => s.UserName == requestData.UserName 
                && s.Password == requestData.Password).FirstOrDefault();
                return user;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserFunction> UserFunction_GetRole(int userID, int functionID)
        {
            return DbContext.UserFunction.Where(s=>s.UserID == userID && s.FunctionID == functionID).FirstOrDefault();
        }
    }
}
