using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class EditFanfic {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required(ErrorMessage = "Chapters required")]
        public List<string> ChaptersId { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        public string GenreId { get; set; }
        public List<string> TagsId { get; set; }
    }
}
