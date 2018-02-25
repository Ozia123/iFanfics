using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.BLL.Interfaces {
    public interface IChapterService : IService<ChapterDTO, string> {
        IQueryable<Chapter> Query();

        IEnumerable<ChapterDTO> GetFanficChapters(string id);
        IEnumerable<ChapterDTO> AddRange(List<ChapterDTO> items);
        Task<List<ChapterRatingDTO>> GetChapterRatingsAsync(string id);
    }
}