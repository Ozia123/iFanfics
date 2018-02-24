using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface ITagService : IService<TagDTO, string> {
        IQueryable<Tag> Query();

        bool CheckForExistingTag(string value);
        TagDTO GetTagByName(string tagName);
        IEnumerable<TagDTO> AddRange(List<TagDTO> items);
        List<TagDTO> GetAll();
    }
}
