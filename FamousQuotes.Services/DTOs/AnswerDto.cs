namespace FamousQuotes.Services.DTOs
{
    public class AnswerDto
    {
        public int QuoteId { get; set; }

        public string AuthorName { get; set; }

        public bool? IsAnswerTrue { get; set; }
    }
}