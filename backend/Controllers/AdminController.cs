using cms_api.Data;
using cms_api.Dto;
using cms_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms_api.Controllers
{
    [ApiController]
    [Route("/api/hyper")]
    public class AdminController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;

        public AdminController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet("/users")]
        public IActionResult Users()
        {
            try
            {
                var users = _dbContext.RootUsers.Select(user => new {email = user.EmailAddress, role = user.Role});
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/user")]
        async public Task<IActionResult> User([FromBody] RootUserDto payload)
        {
            try
            {
                RootUser user = new();
                user.EmailAddress = payload.Email;
                user.Username = payload.Username;
                user.Password = payload.Password;
                user.Role = payload.Role;
                user.Id = Guid.NewGuid().ToString();

                _dbContext.RootUsers.Add(user);
                await _dbContext.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/meta")]
         public IActionResult MetaGet()
        {
            try
            {
                var data = _dbContext.Meta.FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("/meta")]
        async public Task<IActionResult> MetaPut([FromBody] Meta payload)
        {
            try
            {
                var data = _dbContext.Meta.FirstOrDefault();
                if(data == null)
                {
                    _dbContext.Meta.Add(payload);
                    _dbContext.SaveChanges();
                    return Ok(payload);
                }
                data.BrandName = payload.BrandName;
                data.BrandDescription = payload.BrandDescription;
                await _dbContext.SaveChangesAsync();
                return Ok(payload);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("/themes")]

        public IActionResult ThemesGet()
        {
            try
            {
                var data = _dbContext.Themes.FirstOrDefault();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("/theme")]
        async public Task<IActionResult> ChangeTheme([FromBody] ChangeThemeDto payload)
        {
            try
            {
                var currentActiveTheme = _dbContext.Themes.Where(theme => theme.isActive).FirstOrDefault();
                var userSelectedTheme = _dbContext.Themes.Where(theme => theme.Id == payload.Id).FirstOrDefault();
                if (userSelectedTheme == null)
                {
                    return NotFound();
                }
                if (currentActiveTheme == null)
                {
                    userSelectedTheme.isActive = true;
                    await _dbContext.SaveChangesAsync();
                    return Ok(userSelectedTheme);
                }
                currentActiveTheme.isActive = false;
                await _dbContext.SaveChangesAsync();
                userSelectedTheme.isActive = true;
                await _dbContext.SaveChangesAsync();
                return Ok(userSelectedTheme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
