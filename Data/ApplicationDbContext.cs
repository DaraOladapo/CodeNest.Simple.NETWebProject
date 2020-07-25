using CodeNest.Simple.NETWebProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeNest.Simple.NETWebProject.Data
{
    public class ApplicationDbContext:DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}