namespace FamousQuotes.Services.DTOs
{
    using FamousQuotes.Infrastructure.BindingModels;
    using FamousQuotes.Infrastructure.Mapping.Contracts;

    public class AnswerDto : IMapFrom<AnswerBindingModel>
    {
        public string QuoteText { get; set; }

        public string AuthorName { get; set; }

        public bool? IsAnswerTrue { get; set; }
    }
}