using System.Collections.Generic;

namespace iFanfics.Web.Models {
    public class FanficModel {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string picture_url { get; set; }
        public string author_username { get; set; }
        public string genre { get; set; }
        public List<string> tags { get; set; }
        public string creation_date { get; set; }
    }
}
