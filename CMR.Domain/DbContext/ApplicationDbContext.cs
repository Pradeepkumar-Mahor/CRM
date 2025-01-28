using CMR.Domain.DataClass;
using CMR.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMR.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Course> Course { get; set; }

        public DbSet<CourseContent> CourseContent { get; set; }

        public DbSet<CourseContentDetails> CourseContentDetails { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Frequency> Frequency { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<OrderHeader> OrderHeader { get; set; }
    }
}