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
            return _context.Fanfics.FirstOrDefault(a => a.Title.Equals(value)) == null;
        }

        public IEnumerable<Fanfic> GetUserFanfics(string userId) {
            return _context.Fanfics.Where(a => a.ApplicationUserId.Equals(userId));
        }

        public List<Fanfic> GetAll() {
            return _context.Fanfics.ToList();
        }
    }
}
