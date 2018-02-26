namespace iFanfics.Web.Models {
    public class ChapterRatingModel {
        public string id { get; set; }
        public string chapterId { get; set; }
        public string username { get; set; }
        public int givenRating { get; set; }
    }
}
