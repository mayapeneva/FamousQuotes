namespace FamousQuotes.App.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class QuotesSeedMiddlewareExtension
    {
        public static IApplicationBuilder UseSeedQuotesMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<QuotesSeedMiddleware>();
        }
    }
}
