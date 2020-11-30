using System;
using System.Collections.Generic;

namespace NewsPortal.Model
{
    public class News
    {
        public int Id { get; set; }

        public string Headline { get; set; }

        public int? AuthorId { get; set; }

        public User? Author { get; set; }

        public string ShortDescription { get; set; }

        public string Body { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
