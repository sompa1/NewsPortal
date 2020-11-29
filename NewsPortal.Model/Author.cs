using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Model
{
    public class Author : User
    {
        public ICollection<News> News { get; set; }
    }
}