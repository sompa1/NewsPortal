using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Bll.Dtos {

    public class NewsDto {

        public int Id { get; set; }
        public string Headline { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int NumberOfComments { get; set; }
        public int PublishYear { get; set; }

    }
}
