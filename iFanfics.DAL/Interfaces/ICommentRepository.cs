using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFanfics.DAL.Interfaces {
    public interface ICommentRepository : IRepository<Comment, string> {
        IEnumerable<Comment> GetFanficComments(string id);
        Task<IEnumerable<CommentRating>> GetCommentRatingsAsync(string id);
    }
}