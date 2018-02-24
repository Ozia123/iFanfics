using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.BLL.Interfaces {
    public interface ICommentService : IService<CommentDTO, string> {
        IQueryable<Comment> Query();

        Task<List<CommentRatingDTO>> GetCommentRatingsAsync(string id);
    }
}
