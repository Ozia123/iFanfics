using System.Collections.Generic;
using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace iFanfics.DAL.Repositories {
    public class FanficRepository : Repository<Fanfic, string>, IFanficRepository {
        public FanficRepository(ApplicationContext db) : base(db) { }

        public bool CheckUniqueTitle(string value) {
            return _context.Fanfics.FirstOrDefault(a => a.Title.Equals(value)) != null;
        }

        public IEnumerable<Fanfic> GetUserFanfics(string userId) {
            return _context.Fanfics.Where(a => a.ApplicationUserId.Equals(userId));
        }

        public async Task<IEnumerable<Chapter>> GetChaptersAsync(string id) {
            var fanfic = await GetById(id);
            return fanfic.Chapters;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(string id) {
            var fanfic = await GetById(id);
            return fanfic.Comments;
        }

        public async Task<IEnumerable<Tag>> GetFanficTagsAsync(string id) {
            var fanfic = await GetById(id);
            if (fanfic.FanficTags == null) {
                return new List<Tag>();
            }

            var tags = new List<Tag>();
            foreach (var fanficTag in fanfic.FanficTags) {
                var tag = await _context.Tags.FindAsync(fanficTag.TagId);
                tags.Add(tag);
            }

            return tags;
        }

        public List<Fanfic> GetAll() {
            return _context.Fanfics.ToList();
        }
    }
}
