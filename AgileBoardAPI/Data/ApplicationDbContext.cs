using AgileBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AgileBoardAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // Other DbSet properties...

        public DbSet<Project> Projects { get; set; }

        public DbSet<ConfirmationToken> ConfirmationTokens { get; set; }
    }



}
