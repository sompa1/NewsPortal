using NewsPortal.Dal.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Web.Models {

    public class NewsModel {
        public NewsDto News { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        //public int? CurrentUserId { get; set; }
    }
}
