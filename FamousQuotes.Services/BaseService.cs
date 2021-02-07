namespace FamousQuotes.Services
{
    using FamousQuotes.Infrastructure.Data;

    public abstract class BaseService
    {
        protected BaseService(FamousQuotesDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected FamousQuotesDbContext DbContext { get; set; }
    }
}
