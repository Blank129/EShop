using DataAccess.EShop.Entities;
using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.Services
{
    public class UserFunctionRepository : IUserFunctionRepository
    {
        private EShopDBContext dBContext;
        public UserFunctionRepository(EShopDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<List<UserFunction>> GetListUserFunction()
        {
            var list = new List<UserFunction>();    
            try
            {
                list = dBContext.UserFunction.ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserFunction> GetUserFunctionById(int id)
        {
            var model = new UserFunction();
            try
            {
                model = dBContext.UserFunction.Where(s=>s.UserFunctionID == id).FirstOrDefault();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReturnData> UserFunction_Update(UserFunction userFunction)
        {
            var returnData = new ReturnData();
            try
            {
                if(userFunction == null || userFunction.UserFunctionID < 0)
                {
                    returnData.returnCode = -1;
                    returnData.returnMessage = "Tài khoản k tồn tại";
                    return returnData;
                }
                dBContext.UserFunction.Update(userFunction);
                dBContext.SaveChanges();
                returnData.returnCode = 1;
                returnData.returnMessage = "Sửa quyền thành công";
                return returnData;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
