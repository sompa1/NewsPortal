using System.Collections.Generic;

namespace NewsPortal.Model {
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<News> News { get; set; }

    }
}
