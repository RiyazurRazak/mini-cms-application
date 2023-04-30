using cms_api.Models;
using Microsoft.EntityFrameworkCore;

namespace cms_api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options) { }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Meta> Meta { get; set; }
        public DbSet<RootUser> RootUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Themes> Themes { get; set; }
    }
}
