using System.Collections.Generic;
using iFanfics.DAL.Entities;

namespace iFanfics.DAL.Interfaces {
    public interface IGenreRepository : IRepository<Genre, string> {
        bool CheckForExistingGenre(string value);
        IEnumerable<Genre> AddRange(List<Genre> items);
        List<Genre> GetAll();
    }
}
