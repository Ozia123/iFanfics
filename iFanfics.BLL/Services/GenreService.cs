using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class GenreService : IGenreService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Genre> Query() {
            return _database.GenreRepository.Query();
        }

        public async Task<GenreDTO> GetById(string id) {
            Genre genre = await _database.GenreRepository.GetById(id);
            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public IEnumerable<GenreDTO> AddRange(List<GenreDTO> items) {
            IEnumerable<Genre> genres = _database.GenreRepository.AddRange(_mapper.Map<List<GenreDTO>, List<Genre>>(items));
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);
        }

        public bool CheckForExistingGenre(string value) {
            return _database.GenreRepository.CheckForExistingGenre(value);
        }

        public GenreDTO GetByName(string genreName) {
            Genre genre = _database.GenreRepository.GetByName(genreName);
            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public async Task<GenreDTO> Create(GenreDTO item) {
            Genre genre = await _database.GenreRepository.Create(_mapper.Map<GenreDTO, Genre>(item));
            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public async Task<GenreDTO> Delete(string id) {
            Genre genre = await _database.GenreRepository.Delete(id);
            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public async Task<GenreDTO> Update(GenreDTO item) {
            Genre genre = await _database.GenreRepository.Update(_mapper.Map<GenreDTO, Genre>(item));
            return _mapper.Map<Genre, GenreDTO>(genre);
        }

        public List<GenreDTO> GetAll() {
            List<Genre> genres = _database.GenreRepository.GetAll();
            return _mapper.Map<List<Genre>, List<GenreDTO>>(genres);
        }
    }
}