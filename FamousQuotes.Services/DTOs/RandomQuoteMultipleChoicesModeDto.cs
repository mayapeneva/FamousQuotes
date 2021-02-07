namespace FamousQuotes.Services.DTOs
{
    public class RandomQuoteMultipleChoicesModeDto
    {
        public QuoteDto Quote { get; set; }

        public AuthorDto AuthorOne { get; set; }

        public AuthorDto AuthorTwo { get; set; }

        public AuthorDto AuthorThree { get; set; }

        public int? Score { get; set; }
    }
}
