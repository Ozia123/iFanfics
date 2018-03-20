using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class ProfileRepository : Repository<ClientProfile, string>, IProfileRepository {
        public ProfileRepository(ApplicationContext db) : base(db) { }

        public List<string> GetAllUsersId() {
            return _context.Users.Select(a => a.Id).ToList();
        }
    }
}
