using System;

namespace NewsPortal.Dal.Entities
{
    public class News
    {
        public int Id { get; set; }

        public string Headline { get; set; }

        public string Author { get; set; }

        public string ShortDescription { get; set; }

        public string Body { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
