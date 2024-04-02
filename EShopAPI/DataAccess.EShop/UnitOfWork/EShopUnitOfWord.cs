using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.UnitOfWork
{
    public class EShopUnitOfWord : IEShopUnitOfWord
    {
        public IUserRepository _userRepository { get; set; }
        public IUserFunctionRepository _userFunctionRepository { get ; set ; }

        public EShopDBContext _eShopDbContext;
        public EShopUnitOfWord(IUserRepository userRepository, IUserFunctionRepository userFunctionRepository ,EShopDBContext eShopDbContext)
        {
            _userRepository = userRepository;
            _userFunctionRepository = userFunctionRepository;
            _eShopDbContext = eShopDbContext;
        }

        public void SaveChange()
        {
            _eShopDbContext.SaveChanges();
        }
        public void Dispose()
        {
            _eShopDbContext.Dispose();
        }
    }
}
