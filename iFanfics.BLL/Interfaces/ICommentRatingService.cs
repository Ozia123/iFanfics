using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface ICommentRatingService : IService<CommentRatingDTO, string> {
        IQueryable<CommentRating> Query();

        IEnumerable<CommentRatingDTO> GetCommentRatings(string id);
        bool CheckForGivenRating(string userId, string commentId);
    }
}
