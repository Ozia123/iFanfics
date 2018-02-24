using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iFanfics.DAL.Entities {
    public class Comment {
        public string Id { get; set; }
        public string FanficId { get; set; }
        public string ApplicationUserId { get; set; }

        [Required]
        public string CommentText { get; set; }

        public virtual List<CommentRating> CommentRating { get; set; }
    }
}
