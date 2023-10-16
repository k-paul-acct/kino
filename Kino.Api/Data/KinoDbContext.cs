# nullable disable

using Kino.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Kino.Api.Data;

public class KinoDbContext : DbContext
{
    public KinoDbContext()
    {
    }

    public KinoDbContext(DbContextOptions<KinoDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<FaveList> FaveLists { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Title> Titles { get; set; }
    public virtual DbSet<TitleHasGenre> TitleHasGenres { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Title).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FaveList>(entity =>
        {
            entity.ToTable("FaveList");

            entity.HasOne(d => d.Title).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Role>(entity => { entity.ToTable("Role"); });

        modelBuilder.Entity<Genre>(entity => { entity.ToTable("Genre"); });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.HasMany(x => x.Genres).WithMany(x => x.Titles).UsingEntity<TitleHasGenre>();
        });

        modelBuilder.Entity<TitleHasGenre>(entity =>
        {
            entity.ToTable("Genre_Title");

            entity.HasKey(x => new { x.GenreId, x.TitleId, });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.ToTable("Vote");

            entity.HasKey(e => new { e.UserId, e.TitleId, });

            entity.HasOne(d => d.Title).WithMany(p => p.Votes)
                .HasForeignKey(d => d.TitleId);

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId);
        });
    }
}