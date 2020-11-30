using NewsPortal.Bll.Dtos;
using System.Collections.Generic;

namespace NewsPortal.Web.Models {

    public class NewsModel {
        public NewsDto News { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public int? CurrentUserId { get; set; }
    }
}
