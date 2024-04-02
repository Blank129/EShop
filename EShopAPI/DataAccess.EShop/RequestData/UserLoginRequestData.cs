using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EShop.RequestData
{
    public class UserLoginRequestData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AccountUpdateRefeshTokenRequestData
    {
        public int UserID { get; set; }
        public string RefeshToken { get; set; }
        public DateTime RefeshTokenExpired { get; set; }
    }
}
