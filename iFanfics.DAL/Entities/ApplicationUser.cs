using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace iFanfics.DAL.Entities {
    public class ApplicationUser : IdentityUser {
        public virtual ClientProfile ClientProfile { get; set; }

        public virtual List<ChapterRating> ChaptersRating { get; set; }
        public virtual List<CommentRating> CommentsRating { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Fanfic> Fanfics { get; set; }
    }
}