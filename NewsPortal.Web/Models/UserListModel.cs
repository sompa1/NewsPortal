using NewsPortal.Model;
using System.Collections.Generic;

namespace NewsPortal.Web.Models {

    public class UserListModel {

        public int CurrentUserId { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
