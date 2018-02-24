using System;
using System.Collections.Generic;

namespace iFanfics.DAL.Entities {
    public class FanficForElastic {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Username { get; set; }
        public string GenreName { get; set; }
        public string ApplicationUserId { get; set; }
        public string GenreId { get; set; }
        
        public virtual List<ChapterForElastic> ChaptersForElastic { get; set; }
        public virtual List<TagForElastic> TagsForElastic { get; set; }
        public virtual List<CommentForElastic> CommentsForElastic { get; set; }
    }
}
