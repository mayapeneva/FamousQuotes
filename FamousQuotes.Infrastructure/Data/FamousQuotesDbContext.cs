namespace FamousQuotes.Infrastructure.Data
{
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FamousQuotesDbContext : IdentityDbContext<User>
    {
        public FamousQuotesDbContext()
        {
        }

        public FamousQuotesDbContext(DbContextOptions<FamousQuotesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Author> Authors { get; set; }

        public override DbSet<User> Users { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FamousQuotes;Trusted_Connection=True");
            }

            optionsBuilder.UseLazyLoadingProxies(true);
        }
    }
}
