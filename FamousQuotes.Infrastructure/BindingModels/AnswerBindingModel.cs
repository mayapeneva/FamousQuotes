namespace FamousQuotes.Infrastructure.BindingModels
{
    public class AnswerBindingModel
    {
        public string QuoteText { get; set; }

        public string AuthorName { get; set; }

        public bool? IsAnswerTrue { get; set; }
    }
}