using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFanfics.DAL.Interfaces {
    public interface IFanficRepository : IRepository<Fanfic, string> {
        bool CheckUniqueTitle(string value);
        IEnumerable<Fanfic> GetUserFanfics(string userId);

        Task<IEnumerable<Chapter>> GetChaptersAsync(string id);
        Task<IEnumerable<Comment>> GetCommentsAsync(string id);
        Task<IEnumerable<Tag>> GetFanficTagsAsync(string id);

        List<Fanfic> GetAll();
    }
}