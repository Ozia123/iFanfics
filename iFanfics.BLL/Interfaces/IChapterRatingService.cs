using iFanfics.BLL.DTO;
using System.Collections.Generic;

namespace iFanfics.BLL.Interfaces {
    public interface IChapterRatingService : IService<ChapterRatingDTO, string> {
        bool CheckForGivenRating(string userId, string chapterId);
        IEnumerable<ChapterRatingDTO> GetChapterRatings(string id);
    }
}
