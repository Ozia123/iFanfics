using iFanfics.BLL.DTO;
using iFanfics.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.BLL.Interfaces {
    public interface IGenreService : IService<GenreDTO, string> {
        IQueryable<Genre> Query();

        bool CheckForExistingGenre(string value);
        IEnumerable<GenreDTO> AddRange(List<GenreDTO> items);
        List<GenreDTO> GetAll();
    }
}
