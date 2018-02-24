using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface ITagService : IService<TagDTO, string> {
        IQueryable<Tag> Query();

        bool CheckForExistingTag(string value);
        IEnumerable<TagDTO> AddRange(List<TagDTO> items);
        List<TagDTO> GetAll();
    }
}
