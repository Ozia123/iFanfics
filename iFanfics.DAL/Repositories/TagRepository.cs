using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class TagRepository : Repository<Tag, string>, ITagRepository {
        public TagRepository(ApplicationContext db) : base(db) { }

        public bool CheckForExistingTag(string value) {
            return _context.Tags.FirstOrDefault(a => a.TagName.Equals(value)) != null;
        }

        public Tag GetByName(string tagName) {
            return _context.Tags.FirstOrDefault(a => a.TagName.Equals(tagName));
        }

        public IEnumerable<Tag> AddRange(List<Tag> items) {
            List<Tag> tags = new List<Tag>();

            foreach (var item in items) {
                tags.Add(_context.Tags.Add(item).Entity);
                _context.SaveChanges();
            }
            return tags;
        }

        public List<Tag> GetAll() {
            return _context.Tags.ToList();
        }
    }
}