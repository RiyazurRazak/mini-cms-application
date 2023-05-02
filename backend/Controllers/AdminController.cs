using cms_api.Data;
using cms_api.Dto;
using cms_api.Models;
using cms_api.Services;
using cms_api.Utils;
using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_api.Controllers
{
   
    [Route("api/hyper")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;

        public AdminController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            try
            {
                var users = _dbContext.RootUsers.Select(user => new {email = user.EmailAddress, role = user.Role, id = user.Id, isMfaEnabled = user.isMfaEnabled});
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("user")]
        async public Task<IActionResult> User([FromBody] RootUserDto payload)
        {
            try
            {

                string hashedPassword = CryptoHelper.HashPassword(payload.Password);
                RootUser user = new();
                user.EmailAddress = payload.Email;
                user.Username = payload.Username;
                user.Password = hashedPassword;
                user.Role = payload.Role;
                user.Id = Guid.NewGuid().ToString();
                _dbContext.RootUsers.Add(user);
                await _dbContext.SaveChangesAsync();
                await EmailService.SendMail(payload.Email, payload.Username, payload.Password);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("user/{id}")]
        async public Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var userToRemove = _dbContext.RootUsers.Find(id);
                _dbContext.RootUsers.Remove(userToRemove!);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("enable-2fa")]

        public IActionResult AddMFA()
        {
            try
            {
                var id = Request.HttpContext.Items["user"];
                Console.WriteLine(id);
                var user = _dbContext.RootUsers.Find(id);
                if(user == null)
                {
                    return NotFound();
                }
                string key = user.Id.Replace("-", "").Substring(0, 10);
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                SetupCode setupInfo = tfa.GenerateSetupCode("Riyaz CMS", user.EmailAddress, key, false, 3);
                return Ok(new
                {
                    qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl,
                    userId = id,
                    setupCode = setupInfo.ManualEntryKey
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("verify-2fa/{code}")]
        async public Task<IActionResult> VerifyMfa(string code)
        {
            try
            {
                var id = (string)Request.HttpContext.Items["user"]!;
                string key = id.Replace("-", "").Substring(0, 10);
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                bool isValidToken = tfa.ValidateTwoFactorPIN(key, code);
                if (isValidToken)
                {
                    var user = _dbContext.RootUsers.Find(id);
                    user.isMfaEnabled = true;
                    await _dbContext.SaveChangesAsync();
                    return Ok(new {status = true});
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("meta")]
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

        [HttpPut("meta")]
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


        [HttpGet("themes")]

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

        [HttpPut("theme")]
        async public Task<IActionResult> ChangeTheme([FromBody] ChangeThemeDto payload)
        {
            try
            {
                Console.WriteLine(payload.Name);
                var currentActiveTheme = _dbContext.Themes.Where(theme => theme.isActive).FirstOrDefault();
                var userSelectedTheme = _dbContext.Themes.Where(theme => theme.Name == payload.Name).FirstOrDefault();
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
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("blogs")]
        public IActionResult AllBlogs()
        {
            try
            {
                var blogs = _dbContext.Articles.Include(x => x.User).Select(x => new { x.Id, author = x.User.Username, x.Title, x.CreatedAt, x.Likes, x.Status });
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("blog")]
        async public Task<IActionResult> AddBlog([FromBody] AddBlogDto payload)
        {
            try
            {
                var article = new Articles();
                article.Id = Guid.NewGuid().ToString();
                article.Title = payload.Title;
                article.Description = payload.Description;
                article.Status = true;
                article.User_Id = (string) Request.HttpContext.Items["user"]!;
                article.Likes = 0;
                article.Body = payload.Body;
                article.CreatedAt = DateTime.Now;
                _dbContext.Articles.Add(article);
                await _dbContext.SaveChangesAsync();
                return Ok(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("blog/{id}")]
        async public Task<IActionResult> UpdateBLog(string id, [FromBody] AddBlogDto payload)
        {
            try
            {
                var article = _dbContext.Articles.Find(id);
                if (article == null)
                {
                    return NotFound();
                }
                article.Title = payload.Title;
                article.Description = payload.Description;
                article.Body = payload.Body;
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("blog/status/{id}")]
        async public Task<IActionResult> ToggleStatus(string id)
        {
            try
            {
                var article = _dbContext.Articles.Find(id);
                if(article == null)
                {
                    return NotFound();
                }
                article.Status = !article.Status;
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("blog/{id}")]
        async public Task<IActionResult> DeleteBlog(string id)
        {
            try
            {
                var article = _dbContext.Articles.Find(id);
                if (article == null)
                {
                    return NotFound();
                }
                _dbContext.Articles.Remove(article);
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("comments")]
        public IActionResult AllComments()
        {
            try
            {
               var comments =  _dbContext.Comments.Include(comment => comment.User).Include(comment => comment.Articles).Select(comment => new {comment.Id, comment.Message, author = comment.User.Name, article = comment.Articles.Title });
                return Ok(comments);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("comment/{id}")]
        public IActionResult DeleteComment(string id)
        {
            try
            {
                var comment = _dbContext.Comments.Find(id);
                if(comment == null)
                {
                    return NotFound();
                }
                _dbContext.Comments.Remove(comment);
                _dbContext.SaveChanges();
                return Ok(new {status = true});

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("comment-users")]
        public IActionResult AllCommentUsers()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                return Ok(users);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


    }
}
