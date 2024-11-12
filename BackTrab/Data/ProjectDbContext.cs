using static TrabalhoBackEnd.Models.Descriptions;
using static TrabalhoBackEnd.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoBackEnd.DAL
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Description { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
