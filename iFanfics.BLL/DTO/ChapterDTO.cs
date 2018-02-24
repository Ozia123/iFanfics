namespace iFanfics.BLL.DTO {
    public class ChapterDTO {
        public string Id { get; set; }
        public string FanficId { get; set; }

        public FanficDTO Fanfic { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public string ChapterText { get; set; }
    }
}