using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Dal.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Headline { get; set; }
        public string ShortDescription { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
