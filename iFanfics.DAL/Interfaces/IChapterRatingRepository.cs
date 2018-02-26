using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface IChapterRatingRepository : IRepository<ChapterRating, string> {
        bool CheckForGivenRating(string userId, string chapterId);
        IEnumerable<ChapterRating> GetChapterRatins(string id);
    }
}