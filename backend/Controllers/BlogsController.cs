using cms_api.Data;
using cms_api.Dto;
using cms_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_api.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public BlogsController(ApplicationDBContext context) { 
            _dbContext = context;
        }

        [HttpGet("")]
        public IActionResult AllBlogs()
        {
            try
            {
                var blogs = _dbContext.Articles.Where(article => article.Status).Select(x => new { x.Id, author = x.User.Username, x.Title, x.CreatedAt, x.Likes, x.Description  });
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("top")]
        public IActionResult TopBlogs()
        {
            try
            {
                var blogs = _dbContext.Articles.Where(article => article.Status).OrderByDescending(x => x.CreatedAt).Take(5).Select(x => new { x.Id, author = x.User.Username, x.Title, x.CreatedAt, x.Likes, x.Description }) ;
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("article/{id}")]
        public IActionResult Blog(string id)
        {
            try
            {
                var blog = _dbContext.Articles.Include(x => x.User).FirstOrDefault(x => x.Id == id);
                if(blog == null)
                {
                    return NotFound();
                }
                if (!blog.Status)
                {
                    return NotFound();
                }
                return Ok(new
                {
                    title = blog.Title,
                    description = blog.Description,
                    body = blog.Body,
                    likes = blog.Likes,
                    comments = blog?.Comments,
                    author = blog?.User?.Username,
                    createdAt = blog.CreatedAt
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("like/{id}")]
        async public Task<IActionResult> LikeBlog(string id)
        {
            try
            {
                var blog = _dbContext.Articles.Find(id);
                if (blog == null)
                {
                    return NotFound();
                }
                blog.Likes = blog.Likes + 1;
                await _dbContext.SaveChangesAsync();
                return Ok(blog);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("comments/{id}")]
        public IActionResult AllComment(string id)
        {
            try
            {
                var comments = _dbContext.Comments.Include(x => x.User).Where(comment => comment.Article_Id == id).Select(comment => new
                {
                    email = comment.User.EmailAddress,
                    name = comment.User.Name,
                    message = comment.Message
                });
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("comment/{id}")]
        async public Task<IActionResult> AddComment(string id, [FromBody] AddCommentDto payload)
        {
            try
            {
                var commentUser = _dbContext.Users.Where(user => user.EmailAddress == payload.EmailAddress).FirstOrDefault();
                if(commentUser == null)
                {
                    User user = new User();
                    user.EmailAddress = payload.EmailAddress;
                    user.Name = payload.Name;
                    user.Id = Guid.NewGuid().ToString();
                    _dbContext.Users.Add(user);
                    await _dbContext.SaveChangesAsync();
                    commentUser = user;
                }
                Comments comment = new();
                comment.Id = Guid.NewGuid().ToString();
                comment.Message = payload.Message;
                comment.Article_Id = id;
                comment.User_Id = commentUser.Id;
                _dbContext.Add(comment);
                await _dbContext.SaveChangesAsync();
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
