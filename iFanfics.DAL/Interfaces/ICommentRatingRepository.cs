using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface ICommentRatingRepository : IRepository<CommentRating, string> {
        IEnumerable<CommentRating> GetCommentRatins(string id);
        bool CheckForGivenRating(string userId, string commentId);
    }
}