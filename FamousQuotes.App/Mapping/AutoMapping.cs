namespace FamousQuotes.App.Mapping
{
    using AutoMapper;
    using FamousQuotes.Infrastructure.BindingModels;
    using FamousQuotes.Infrastructure.ViewModels;
    using FamousQuotes.Services.DTOs;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<QuoteDto, QuoteViewModel>();
        }
    }
}