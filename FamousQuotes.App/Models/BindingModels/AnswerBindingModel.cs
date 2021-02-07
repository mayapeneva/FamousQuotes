namespace FamousQuotes.App.Models.BindingModels
{
    public class AnswerBindingModel
    {
        public int QuoteId { get; set; }

        public string QuoteText { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public bool IsAnswerTrue { get; set; }
    }
}

