namespace FamousQuotes.Services.DTOs
{
    public class RandomQuoteBinaryModeDto
    {
        public QuoteDto Quote { get; set; }

        public AuthorDto Author { get; set; }

        public int? Score { get; set; }
    }
}
