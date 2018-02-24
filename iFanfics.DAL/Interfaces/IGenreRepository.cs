using System.Collections.Generic;
using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface IGenreRepository : IRepository<Genre, string> {
        bool CheckForExistingGenre(string value);
        Genre GetByName(string genreName);
        IEnumerable<Genre> AddRange(List<Genre> items);
        List<Genre> GetAll();
    }
}
