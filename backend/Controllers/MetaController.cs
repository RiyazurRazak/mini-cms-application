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
    }
}
