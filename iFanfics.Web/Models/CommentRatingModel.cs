namespace iFanfics.Web.Models {
    public class CommentRatingModel {
        public string id { get; set; }
        public string commentId { get; set; }
        public string username { get; set; }
        public int givenRating { get; set; }
    }
}
