using System.Collections.Generic;
using iFanfics.DAL.Entities;
using System.Threading.Tasks;

namespace iFanfics.DAL.Interfaces {
    public interface IChapterRepository : IRepository<Chapter, string> {
        IEnumerable<Chapter> GetFanficChapters(string id);
        Task<IEnumerable<ChapterRating>> GetChapterRatingsAsync(string id);
        IEnumerable<Chapter> AddRange(List<Chapter> items);
    }
}
