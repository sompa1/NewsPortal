using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace NewsPortal.Model {
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<News> News { get; set; }
    }
}
