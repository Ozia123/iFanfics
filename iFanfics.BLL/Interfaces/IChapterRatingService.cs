using iFanfics.BLL.DTO;

namespace iFanfics.BLL.Interfaces {
    public interface IChapterRatingService : IService<ChapterRatingDTO, string> {
        bool CheckForGivenRating(string userId, string chapterId);
    }
}
