using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface ICommentService : IService<CommentDTO, string> {
        IQueryable<Comment> Query();

        IEnumerable<CommentDTO> GetFanficComments(string id);
    }
}
