using cms_api.Data;
using Microsoft.AspNetCore.Mvc;

namespace cms_api.Controllers
{

    [Route("api/pages")]
    public class PagesController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public PagesController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet("page/{slug}")]
        public IActionResult Page(string slug)
        {
            try
            {
                var page = _dbContext.Pages.Where(page => page.Slug == slug && page.IsPublic).FirstOrDefault();
                if(page == null)
                {
                    return NotFound();
                }
                return Ok(page);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
