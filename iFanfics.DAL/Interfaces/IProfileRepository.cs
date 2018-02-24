using System.Threading.Tasks;
using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface IProfileRepository : IRepository<ClientProfile, string> {
        Task<IEnumerable<Fanfic>> GetAllUsersFanficsAsync(string id);
        Task<IEnumerable<Comment>> GetAllUsersCommentsAsync(string id);
        List<string> GetAllUsersId();
    }
}