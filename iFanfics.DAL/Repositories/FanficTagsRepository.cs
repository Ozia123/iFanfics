using System.Collections.Generic;
using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class FanficTagsRepository : Repository<FanficTags, string>, IFanficTagsRepository {
        public FanficTagsRepository(ApplicationContext db) : base(db) { }

        public IEnumerable<FanficTags> GetFanficTagsByFanficId(string id) {
            return _context.FanficsTags.Where(a => a.FanficId.Equals(id));
        }

        public List<FanficTags> GetAll() {
            return _context.FanficsTags.ToList();
        }
    }
}
