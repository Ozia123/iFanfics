using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class CreateComment {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FanficId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "Comment text is required")]
        public string CommentText { get; set; }
    }
}
