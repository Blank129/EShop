using DataAccess.EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.IServices
{
    public interface IUserFunctionRepository
    {
        Task<List<UserFunction>> GetListUserFunction();
        Task<UserFunction> GetUserFunctionById(int id);
        Task<ReturnData> UserFunction_Update(UserFunction userFunction);
    }
}
