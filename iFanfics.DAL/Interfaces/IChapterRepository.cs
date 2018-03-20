using System.Collections.Generic;
using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface IChapterRepository : IRepository<Chapter, string> {
        IEnumerable<Chapter> GetFanficChapters(string id);
        IEnumerable<Chapter> AddRange(List<Chapter> items);
    }
}
