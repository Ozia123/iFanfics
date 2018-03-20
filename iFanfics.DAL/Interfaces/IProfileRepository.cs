using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface IProfileRepository : IRepository<ClientProfile, string> {
        List<string> GetAllUsersId();
    }
}