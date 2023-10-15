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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comment_Title");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<FaveList>(entity =>
        {
            entity.ToTable("FaveList");

            entity.HasOne(d => d.Title).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FaveList_Title");

            entity.HasOne(d => d.User).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FaveList_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.HasMany(d => d.Titles).WithMany(p => p.Genres)
                .UsingEntity<Dictionary<string, object>>(
                    "GenreTitle",
                    r => r.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .HasConstraintName("FK_Genre_Title_Title"),
                    l => l.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_Genre_Title_Genre"),
                    j =>
                    {
                        j.HasKey("GenreId", "TitleId").HasName("PK__Genre_Ti__74D25DE6D3F00BC4");
                        j.ToTable("Genre_Title");
                    });
        });

        modelBuilder.Entity<Role>(entity => { entity.ToTable("Role"); });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.Property(e => e.TitleAdditionalName).HasColumnName("TItleAdditionalName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TitleId, });

            entity.ToTable("Vote");

            entity.HasOne(d => d.Title).WithMany(p => p.Votes)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_Vote_Title");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Vote_User");
        });
    }
}