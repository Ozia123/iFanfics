using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    class CommentRatingRepository : Repository<CommentRating, string>, ICommentRatingRepository {
        public CommentRatingRepository(ApplicationContext db) : base(db) { }

        public bool CheckForGivenRating(string userId, string chapterId) {
            return _context.CommentsRating.FirstOrDefault(a => a.ApplicationUserId.Equals(userId) && a.CommentId.Equals(chapterId)) != null;
        }
    }
}
