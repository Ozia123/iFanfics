namespace iFanfics.BLL.DTO {
    public class CommentDTO {
        public string Id { get; set; }
        public string FanficId { get; set; }
        public string ApplicationUserId { get; set; }

        public string CommentText { get; set; }
    }
}