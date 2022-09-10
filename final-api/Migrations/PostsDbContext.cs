using final_api.Models;
using Microsoft.EntityFrameworkCore;

namespace final_api.Migrations;

public class PostsDbContext : DbContext 
{
    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }

    public PostsDbContext(DbContextOptions<PostsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Post>(entity => 
        {
            entity.HasKey(e => e.PostId);
            // Not sure if this is right of if I just need the WithMany() without the foreign key?
            entity.HasOne(e => e.User).WithMany(p => p.Posts).HasForeignKey(e => e.UserId).IsRequired();
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(280);
            entity.Property(e => e.PostedDate);
        });
    
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(x => x.Username).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);
            entity.Property(e => e.State).HasMaxLength(2);
            entity.Property(e => e.PhotoUrl);
        });
    }
}