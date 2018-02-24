using System.Collections.Generic;

namespace iFanfics.Web.Models {
    public class CurrentUser {
        public IList<string> roles { get; set; }
        public bool isAuntificated { get; set; }
        public string UserName { get; set; }
        public string PictureURL { get; set; }

        public CurrentUser() {
            roles = new List<string>();
            isAuntificated = false;
            UserName = string.Empty;
            PictureURL = string.Empty;
        }
    }
}
