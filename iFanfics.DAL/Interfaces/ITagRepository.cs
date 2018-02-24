using System.Collections.Generic;
using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface ITagRepository : IRepository<Tag, string> {
        bool CheckForExistingTag(string value);
        Tag GetByName(string tagName);
        IEnumerable<Tag> AddRange(List<Tag> items);
        List<Tag> GetAll();
    }
}