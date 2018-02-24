using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iFanfics.DAL.Entities {
    public class Chapter {
        public string Id { get; set; }
        public string FanficId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int ChapterNumber { get; set; }

        [Required]
        public string ChapterText { get; set; }

        public virtual List<ChapterRating> ChapterRating { get; set; }
    }
}
