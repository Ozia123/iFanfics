﻿using System.ComponentModel.DataAnnotations;

namespace iFanfics.Web.Models {
    public class EditCommentRating {
        [Required]
        public string Id { get; set; }
        [Required]
        public string CommentId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int GivenRating { get; set; }
    }
}
