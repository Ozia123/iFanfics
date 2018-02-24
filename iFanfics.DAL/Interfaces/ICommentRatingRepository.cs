using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface ICommentRatingRepository : IRepository<CommentRating, string> {
        bool CheckForGivenRating(string userId, string commentId);
    }
}