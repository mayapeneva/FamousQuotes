namespace FamousQuotes.Infrastructure.Data
{
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class FamousQuotesDbContext : IdentityDbContext<FamousQuotesUser>
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

        public DbSet<FamousQuotesUser> FamousQuotesUsers { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FamousQuotes;Trusted_Connection=True");
            }

            optionsBuilder.UseLazyLoadingProxies(true);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FamousQuotesUser>(u =>
            {
                u.ToTable(nameof(FamousQuotesUser));
                u.HasMany(u => u.Answers)
                .WithOne(a => a.FamousQuotesUser)
                .HasForeignKey(u => u.FamousQuotesUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Answer>(a =>
            {
                a.HasOne(a => a.FamousQuotesUser)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.FamousQuotesUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
