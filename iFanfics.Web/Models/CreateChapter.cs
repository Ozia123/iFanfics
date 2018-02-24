using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class CreateChapter {
        public string FanficId { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Chapter text is required")]
        public string ChapterText { get; set; }
    }
}