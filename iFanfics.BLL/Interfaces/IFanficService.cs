using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.BLL.Interfaces {
    public interface IFanficService : IService<FanficDTO, string> {
        IQueryable<Fanfic> Query();
        bool CheckUniqueName(string value);

        IEnumerable<FanficDTO> GetUserFanfics(string userId);

        List<FanficDTO> GetAll();
    }
}
