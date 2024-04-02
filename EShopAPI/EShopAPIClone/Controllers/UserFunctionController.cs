using DataAccess.EShop.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;
using DataAccess.EShop.Entities;

namespace EShopAPIClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFunctionController : ControllerBase
    {
        private IEShopUnitOfWord _unitOfWork;
        public UserFunctionController(IEShopUnitOfWord unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetListUserFunction")]
        public async Task<ActionResult> GetListUserFunction()
        {
            var list = new List<UserFunction>();
            list = await _unitOfWork._userFunctionRepository.GetListUserFunction();      
            return Ok(list);
        }
        [HttpPost("GetUserFunctionById")]
        public async Task<ActionResult> GetUserFunctionById(int id)
        {
            var model = new UserFunction();
            model = await _unitOfWork._userFunctionRepository.GetUserFunctionById(id);
            return Ok(model);
        }
        [HttpPost("UserFunction_Update")]
        public async Task<ActionResult> UserFunction_Update(UserFunction userFunction)
        {
            var returnData = new ReturnData();
            returnData = await _unitOfWork._userFunctionRepository.UserFunction_Update(userFunction);
            return Ok(returnData);
        }
    }
}
