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
        Task<List<ChapterDTO>> GetChapters(string id);
        Task<List<CommentDTO>> GetComments(string id);
        Task<List<TagDTO>> GetFanficTags(string id);

        List<FanficDTO> GetAll();
    }
}
