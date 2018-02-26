using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.DAL.Repositories {
    public class CommentRepository : Repository<Comment, string>, ICommentRepository {
        public CommentRepository(ApplicationContext db) : base(db) { }

        public IEnumerable<Comment> GetFanficComments(string id) {
            return _context.Comments.Where(a => a.FanficId.Equals(id));
        }

        public async Task<IEnumerable<CommentRating>> GetCommentRatingsAsync(string id) {
            var comment = await GetById(id);
            return comment.CommentRating;
        }
    }
}