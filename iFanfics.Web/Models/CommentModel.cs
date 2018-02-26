using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class CommentModel {
        public string id { get; set; }
        public string username { get; set; }
        public string pictureUrl { get; set; }
        public string comment { get; set; }
    }
}
