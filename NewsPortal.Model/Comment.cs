using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreationDate { get; set; }

    }
}
