using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface IFanficTagsRepository : IRepository<FanficTags, string> {
        IEnumerable<FanficTags> GetFanficTagsByFanficId(string id);
        List<FanficTags> GetAll();
    }
}
