using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class ProfileRepository : Repository<ClientProfile, string>, IProfileRepository {
        public ProfileRepository(ApplicationContext db) : base(db) { }

        public async Task<IEnumerable<Fanfic>> GetAllUsersFanficsAsync(string id) {
            var user = await GetById(id);
            return user.ApplicationUser.Fanfics;
        }

        public async Task<IEnumerable<Comment>> GetAllUsersCommentsAsync(string id) {
            var user = await GetById(id);
            return user.ApplicationUser.Comments;
        }

        public List<string> GetAllUsersId() {
            return _context.Users.Select(a => a.Id).ToList();
        }
    }
}
