using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class ChapterRepository : Repository<Chapter, string>, IChapterRepository {
        public ChapterRepository(ApplicationContext db) : base(db) { }

        public IEnumerable<Chapter> GetFanficChapters(string id) {
            return _context.Chapters.Where(a => a.FanficId.Equals(id));
        }

        public IEnumerable<Chapter> AddRange(List<Chapter> items) {
            List<Chapter> chapters = new List<Chapter>();

            foreach (var item in items) {
                chapters.Add(_context.Chapters.Add(item).Entity);
                _context.SaveChanges();
            }
            return chapters;
        }
    }
}
