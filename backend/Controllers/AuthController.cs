using cms_api.Data;
using cms_api.Dto;
using cms_api.Utils;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_api.Controllers
{

    [Route("api/auth")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public AuthController(ApplicationDBContext context)
        {
            _dbContext = context;
        }


        [HttpGet("login")]
        public IActionResult Login([FromQuery] LoginDto payload)
        {
            try
            {
                var user = _dbContext.RootUsers.Where(user => user.Username == payload.Username).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                if(!CryptoHelper.ValidatePassword(payload.Password, user.Password)){
                    return Unauthorized();
                }
                if (user.isMfaEnabled)
                {
                    return Ok(new
                    {
                        name = user.Username,
                        id = user.Id,
                        isMfa = true,
                    });
                }
                string token = TokenHelper.GenerateToken(user.Id, user.Role);
                return Ok(new
                {
                    name = user.Username,
                    email = user.EmailAddress,
                    token,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("verify")]
        public IActionResult VerifyMFA([FromBody] VerifyDto payload)
        {
            try
            {
                var user = _dbContext.RootUsers.Where(user => user.Id == payload.Id).FirstOrDefault();
                if (user == null)
                {
                    return NotFound();
                }
                string key = user.Id.Replace("-", "").Substring(0, 10);
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                if (!tfa.ValidateTwoFactorPIN(key, payload.Code))
                {
                    return Unauthorized();
                }
                string token = TokenHelper.GenerateToken(user.Id, user.Role);
                return Ok(new
                {
                    name = user.Username,
                    email = user.EmailAddress,
                    token,
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
