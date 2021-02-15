namespace FamousQuotes.App.Extensions
{
    using FamousQuotes.Infrastructure.Data;
    using FamousQuotes.Infrastructure.Models;
    using Microsoft.AspNetCore.Http;
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
            if (dbContext.Quotes.Count() == 0)
            {
                QuotesSeed();
            }

            await next(context);
        }

        private void QuotesSeed()
        {
            var jsonString = File.ReadAllText(@"Extensions\famousQuotes.json");
            var quotesGroupedByAuthor = JsonConvert.DeserializeObject<QuoteSeedDto[]>(jsonString).GroupBy(q => q.QuoteAuthor);

            var authors = new List<Author>();
            foreach (var quotes in quotesGroupedByAuthor)
            {
                var authorName = quotes.Key;
                var author = authors.FirstOrDefault(a => a.Name == authorName);
                if (author is null)
                {
                    author = new Author(authorName);
                }

                quotes.Select(q => q.QuoteText).ToHashSet().ToList().ForEach(qt => author.Quotes.Add(new Quote(author.Id, qt)));
                authors.Add(author);
            }

            dbContext.Authors.AddRange(authors);
            dbContext.SaveChanges();
        }
    }
}
