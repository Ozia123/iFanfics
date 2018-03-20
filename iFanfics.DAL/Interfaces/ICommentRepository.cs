using iFanfics.DAL.Entities;
using System.Collections.Generic;

namespace iFanfics.DAL.Interfaces {
    public interface ICommentRepository : IRepository<Comment, string> {
        IEnumerable<Comment> GetFanficComments(string id);
    }
}