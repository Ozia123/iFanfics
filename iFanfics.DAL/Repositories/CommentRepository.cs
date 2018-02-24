using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iFanfics.DAL.Repositories {
    public class CommentRepository : Repository<Comment, string>, ICommentRepository {
        public CommentRepository(ApplicationContext db) : base(db) { }

        public async Task<IEnumerable<CommentRating>> GetCommentRatingsAsync(string id) {
            var comment = await GetById(id);
            return comment.CommentRating;
        }
    }
}