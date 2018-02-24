namespace iFanfics.BLL.DTO {
    public class ChapterRatingDTO {
        public string Id { get; set; }
        public string ChapterId { get; set; }
        public string ApplicationUserId { get; set; }

        public int GivenRating { get; set; }
    }
}