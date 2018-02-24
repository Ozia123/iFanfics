using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.Web.Models {
    public class CreateChapterRating {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ChapterId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int GivenRating { get; set; }
    }
}
