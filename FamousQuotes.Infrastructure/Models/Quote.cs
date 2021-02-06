namespace FamousQuotes.Infrastructure.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Quote
    {
        public Quote(int authorId, string text)
        {
            AuthorId = authorId;
            Text = text;
        }

        [Key]
        public int Id { get; private set; }

        public string Text { get; private set; }

        [Required]
        public int AuthorId { get; private set; }


        [ForeignKey(nameof(AuthorId))]
        public virtual Author Author { get; private set; }
    }
}
