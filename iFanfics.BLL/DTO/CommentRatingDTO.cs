﻿namespace iFanfics.BLL.DTO {
    public class CommentRatingDTO {
        public string Id { get; set; }
        public string ChapterId { get; set; }
        public string ApplicationUserId { get; set; }

        public int GivenRating { get; set; }
    }
}