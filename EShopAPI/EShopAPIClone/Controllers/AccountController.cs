using DataAccess.EShop.EntitiesFramework;
using DataAccess.EShop.RequestData;
using DataAccess.EShop.UnitOfWork;
using EShopAPIClone.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EShopAPIClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IEShopUnitOfWord _unitOfWord;
        private IConfiguration _configuration;
        public AccountController(IEShopUnitOfWord unitOfWord, IConfiguration configuration)
        {
            _unitOfWord = unitOfWord;
            _configuration = configuration;
        }
        [HttpPost("Account_Login")]
        public async Task<ActionResult> Account_Login(UserLoginRequestData requestData)
        {
            var returnData = new UserLoginReturnData();
            try
            {
                if(requestData == null && 
                    string.IsNullOrEmpty(requestData.UserName) &&
                    string.IsNullOrEmpty(requestData.Password))
                {
                    return Ok(new { msg = "thông tin tài khoản ko hợp lệ" });
                }
                var user = await _unitOfWord._userRepository.Login(requestData);

                if(user == null || user.UserId < 0)
                {
                    return Ok(new { msg = "thông tin tài khoản ko tồn tại" });
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                };
                var newAccessToken = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
                var refreshToken = GenerateRefreshToken();

                //Cập nhật lại refreshToken vào db
                var expriredDateSettingDay = _configuration["JWT:RefreshTokenValidityInDays"] ?? "";
                var Req = new AccountUpdateRefeshTokenRequestData
                {
                    UserID = user.UserId,
                    RefeshToken = refreshToken,
                    RefeshTokenExpired = DateTime.Now.AddDays(Convert.ToInt32(expriredDateSettingDay))

                };
                var update = await _unitOfWord._userRepository.AccountUpdateRefeshToken(Req);


                returnData.userName = user.UserName;
                returnData.token = token;
                returnData.refreshToken = refreshToken;
                returnData.IsAdmin = user.IsAdmin;
                return Ok(returnData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
