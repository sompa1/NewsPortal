using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Model
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
