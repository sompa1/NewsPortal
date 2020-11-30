using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Bll.Dtos;
using NewsPortal.Dal.Specifications;
using System.Collections.Generic;

namespace NewsPortal.Web.Models {

    public class NewsIndexModel
    {
        public PagedResult<NewsDto> News { get; set; }
        public NewsSpecification Specification { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
