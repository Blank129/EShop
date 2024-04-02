using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public interface IEShopUnitOfWord
    {
        public IUserRepository _userRepository {  get; set; }
        public IUserFunctionRepository _userFunctionRepository { get; set; }
        void SaveChange();
    }
}
