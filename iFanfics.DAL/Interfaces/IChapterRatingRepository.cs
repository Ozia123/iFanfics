using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface IChapterRatingRepository : IRepository<ChapterRating, string> {
        bool CheckForGivenRating(string userId, string chapterId);
    }
}