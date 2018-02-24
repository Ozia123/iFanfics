using System.ComponentModel.DataAnnotations;

namespace iFanfics.DAL.Entities {
    public class CommentRating {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string CommentId { get; set; }

        [Required]
        public int GivenRating { get; set; }
    }
}
