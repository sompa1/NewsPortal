using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Web.Models {
    public class CreateNewsModel
    {
        [Required]
        public string Headline { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
