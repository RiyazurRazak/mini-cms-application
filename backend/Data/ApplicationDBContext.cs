using Microsoft.EntityFrameworkCore;

namespace cms_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) { }
    }
}
