namespace FamousQuotes.Infrastructure.Data
{
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class FamousQuotesDbContext : DbContext
    {
        public FamousQuotesDbContext()
        {
        }

        public FamousQuotesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Author> Authors { get; set; }
    }
}
