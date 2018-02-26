using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface IFanficTagsService : IService<FanficTagsDTO, string> {
        IQueryable<FanficTags> Query();

        IEnumerable<FanficTagsDTO> GetFanficTagsByFanficId(string id);
        List<FanficTagsDTO> GetAll();
    }
}
