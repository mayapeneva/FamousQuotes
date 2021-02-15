namespace FamousQuotes.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Answer
    {
        public Answer(int quoteId, string userId)
        {
            UserId = userId;
            QuoteId = quoteId;
            IsAnswered = false;
        }

        [Required]
        public string UserId { get; private set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }

        [Key]
        public int Id { get; private set; }

        [Required]
        public int QuoteId { get; private set; }

        public bool IsAnswered { get; private set; }

        public string AuthorAsAnswered { get; private set; }

        public bool? IsAnswerTrue { get; private set; }

        public void AddAnswerInfo(string authorAsAnswered, bool? isAnswerTrue)
        {
            AuthorAsAnswered = authorAsAnswered;
            IsAnswerTrue = isAnswerTrue;
            IsAnswerTrue = true;
        }
    }
}
