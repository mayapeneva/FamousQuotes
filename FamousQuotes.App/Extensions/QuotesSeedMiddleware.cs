namespace FamousQuotes.App.Extensions
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Internal;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuotesSeedMiddleware
    {
        private readonly RequestDelegate next;
        private readonly FamousQuotesDbContext dbContext;

        public QuotesSeedMiddleware(RequestDelegate next)
        {
            this.next = next;
            dbContext = new FamousQuotesDbContext();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!dbContext.Authors.Any())
            {
                QuotesSeed();
            }

            await next(context);
        }

        private void QuotesSeed()
        {
            var jsonString = File.ReadAllText(@"Extensions\famousQuotes.json");
            var quoteSeeds = JsonConvert.DeserializeObject<QuoteSeedDto[]>(jsonString);

            var authors = new List<Author>();
            foreach (var quoteSeed in quoteSeeds)
            {
                var authorName = quoteSeed.QuoteAuthor;
                var author = authors.FirstOrDefault(a => a.Name == authorName);
                if (author is null)
                {
                    author = new Author(authorName);
                }

                author.Quotes.Add(new Quote(author.Id, quoteSeed.QuoteText));
                authors.Add(author);
            }

            dbContext.Authors.AddRange(authors);
            dbContext.SaveChanges();
        }
    }
}
