namespace FamousQuotes.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Answer
    {
        public Answer(int quoteId, string authorAsAnswered)
        { 
            QuoteId = quoteId;
            AuthorAsAnswered = authorAsAnswered;
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int QuoteId { get; private set; }

        [ForeignKey(nameof(QuoteId))]
        public virtual Quote Quote { get; private set; }

        public string AuthorAsAnswered { get; private set; }
    }
}
