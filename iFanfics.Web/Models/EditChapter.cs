using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class EditChapter {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FanficId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Chapter text is required")]
        public string ChapterText { get; set; }
    }
}