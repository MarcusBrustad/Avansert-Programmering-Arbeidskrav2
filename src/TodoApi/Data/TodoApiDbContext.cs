using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class TodoApiDbContext(DbContextOptions<TodoApiDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .ToTable("users")
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TodoTask>()
            .ToTable("tasks")
            .Property(t => t.IsCompleted)
            .HasDefaultValue(false);
    }
}