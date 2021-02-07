namespace FamousQuotes.Services.DTOs
{
    public class AnswerDto
    {
        public QuoteDto Quote { get; set; }

        public AuthorDto Author { get; set; }

        public bool IsAnswerTrue { get; set; }
    }
}
