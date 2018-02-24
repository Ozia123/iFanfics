using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class CreateChapter {
        [Required]
        public string FanficId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Chapter text is required")]
        public string ChapterText { get; set; }
    }
}