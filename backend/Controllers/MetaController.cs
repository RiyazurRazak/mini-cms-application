using cms_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace cms_api.Controllers
{
    [Route("/api/meta")]
    [ApiController]
    public class MetaController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;
        public MetaController(ApplicationDBContext context) {
            _dbContext = context;
        }

        [HttpGet("theme")]
        public IActionResult ActiveTheme()
        {
            try
            {
               var activeTheme =  _dbContext.Themes.Where(theme => theme.isActive).FirstOrDefault();
               return Ok(activeTheme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("")]
        public IActionResult MetaDetails()
        {
            try
            {
                var brandDetails = _dbContext.Meta.FirstOrDefault();
                var totalComments = _dbContext.Comments.Count();
                var totalLikes = _dbContext.Articles.Sum(article => article.Likes);
                var totalArticles = _dbContext.Articles.Count();
                var pages = _dbContext.Pages.Where(page => page.IsPublic).Select(page => new { page.Title, page.Slug });
                return Ok(new
                {
                    brandDetails,
                    totalComments,
                    totalLikes,
                    totalArticles,
                    pages,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
