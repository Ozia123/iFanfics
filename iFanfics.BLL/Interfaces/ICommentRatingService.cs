using iFanfics.BLL.DTO;

namespace iFanfics.BLL.Interfaces {
    public interface ICommentRatingService : IService<CommentRatingDTO, string> {
        bool CheckForGivenRating(string userId, string commentId);
    }
}
