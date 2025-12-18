using CleanShortener.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanShortener.Data;

public class CleanShortenerDbContextFactory : IDesignTimeDbContextFactory<CleanShortenerDbContext>
{
    public CleanShortenerDbContext CreateDbContext(string[] args)
    {
        return new CleanShortenerDbContext(
            new DbContextOptionsBuilder<CleanShortenerDbContext>()
            .UseSqlite("Data Source=C:\\CleanShortener\\CleanShortener.db")
            .Options);
    }
}
