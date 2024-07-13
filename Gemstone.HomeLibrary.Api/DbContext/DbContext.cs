using Gemstone.HomeLibrary.Api.Models.HomeLibrary;
using Microsoft.EntityFrameworkCore;

namespace Gemstone.HomeLibrary.Api.DbContext;

public class LibraryDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public LibraryDbContext()
    {
        var folder = AppDomain.CurrentDomain.BaseDirectory;
        DbPath = Path.Join(folder, "library.db");
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ReadBook> ReadBooks { get; set; }

    private string DbPath { get; }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}
