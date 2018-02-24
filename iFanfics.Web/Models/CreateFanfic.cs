using System.Collections.Generic;

namespace iFanfics.Web.Models {
    public class CreateFanfic {
        public string title { get; set; }
        public string description { get; set; }
        public string pictureUrl { get; set; }
        public string genre { get; set; }
        public IEnumerable<string> tags { get; set; }
    }
}