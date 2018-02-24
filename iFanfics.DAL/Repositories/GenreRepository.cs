using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    public class GenreRepository : Repository<Genre, string>, IGenreRepository {
        public GenreRepository(ApplicationContext db) : base(db) { }

        public bool CheckForExistingGenre(string value) {
            return _context.Genres.FirstOrDefault(a => a.GenreName.Equals(value)) != null;
        }

        public Genre GetByName(string genreName) {
            return _context.Genres.FirstOrDefault(a => a.GenreName.Equals(genreName));
        }

        public IEnumerable<Genre> AddRange(List<Genre> items) {
            List<Genre> genres = new List<Genre>();

            foreach (var item in items) {
                genres.Add(_context.Genres.Add(item).Entity);
                _context.SaveChanges();
            }
            return genres;
        }

        public List<Genre> GetAll() {
            return _context.Genres.ToList();
        }
    }
}
