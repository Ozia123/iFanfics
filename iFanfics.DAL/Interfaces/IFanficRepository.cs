using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface IFanficRepository : IRepository<Fanfic, string> {
        bool CheckUniqueTitle(string value);
        IEnumerable<Fanfic> GetUserFanfics(string userId);

        List<Fanfic> GetAll();
    }
}