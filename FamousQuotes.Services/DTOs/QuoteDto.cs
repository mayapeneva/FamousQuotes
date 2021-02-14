namespace FamousQuotes.Services.DTOs
{
    public class QuoteDto
    {
        public int QuoteId { get; set; }

        public string QuoteText { get; set; }

        public string AuthorOneName { get; set; }

        public string AuthorTwoName { get; set; }

        public string AuthorThreeName { get; set; }

        public int? Score { get; set; }
    }
}
