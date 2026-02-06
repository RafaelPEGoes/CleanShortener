using Microsoft.EntityFrameworkCore;
using CleanShortener.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CleanShortener.Data.DbContexts;

public class CleanShortenerDbContext : IdentityDbContext<IdentityUser>
{
    public CleanShortenerDbContext(DbContextOptions<CleanShortenerDbContext> options) : base(options)
    {
    }

    public DbSet<ShortUrl> ShortUrls { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ShortUrl>(entity =>
        {
            entity.ToTable("ShortUrls");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.OriginalUrl)
            .IsRequired()
            .HasColumnType("TEXT");

            entity.Property(e => e.ShortenedUrl)
                .IsRequired()
                .HasColumnType("TEXT")
                .HasMaxLength(100);

            entity.HasIndex(e => e.ShortenedUrl);

            entity.HasIndex(e => e.OriginalUrl);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\CleanShortener\\CleanShortener.db");
        }

        base.OnConfiguring(optionsBuilder);
    }
}
