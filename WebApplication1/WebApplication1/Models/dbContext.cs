using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {

        }
        public DbSet<RegisterUsersModel> registerUsers{ get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
