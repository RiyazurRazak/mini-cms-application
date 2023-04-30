using cms_api.Data;
using cms_api.Dto;
using cms_api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_api.Controllers
{

    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public AuthController(ApplicationDBContext context)
        {
            _dbContext = context;
        }


        [HttpGet]
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
