namespace FamousQuotes.Infrastructure.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public Author(string name)
        {
            Name = name;
            Quotes = new HashSet<Quote>();
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public virtual ICollection<Quote> Quotes { get; private set; }
    }
}
