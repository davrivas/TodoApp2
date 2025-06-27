using Microsoft.EntityFrameworkCore;
using TodoApp.DataAccess.Entities;

namespace TodoApp.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.ToTable("TodoItem", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Item).IsRequired();
                entity.Property(e => e.IsCompleted).IsRequired();
            });
        }
    }
}
