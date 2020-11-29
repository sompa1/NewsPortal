using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Dal.Specifications {

    public class NewsSpecification
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; } = 4;

        public string Author { get; set; }
        public string Headline { get; set; }
        public int? CategoryId { get; set; }
        public int? MinNumberOfComments { get; set; }
        public int? MaxNumberOfComments { get; set; }
        public int? MinPublishYear { get; set; }
        public int? MaxPublishYear { get; set; }
        public NewsOrder? Order { get; set; }
        public enum NewsOrder
        {
            AuthorAscending,
            AuthorDescending,
            PublishYearAscending,
            PublishYearDescending,
            NumberOfCommentsDescending,
            NumberOfCommentsAscending
        }
    }
}
