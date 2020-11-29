﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Bll.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int NewsId { get; set; }
    }
}
