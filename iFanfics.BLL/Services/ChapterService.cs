using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class ChapterService : IChapterService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Chapter> Query() {
            return _database.ChapterRepository.Query();
        }

        public IEnumerable<ChapterDTO> AddRange(List<ChapterDTO> items) {
            IEnumerable<Chapter> chapters = _database.ChapterRepository.AddRange(_mapper.Map<List<ChapterDTO>, List<Chapter>>(items));
            return _mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterDTO>>(chapters);
        }

        public async Task<ChapterDTO> GetById(string id) {
            Chapter chapter = await _database.ChapterRepository.GetById(id);
            return _mapper.Map<Chapter, ChapterDTO>(chapter);
        }

        public async Task<ChapterDTO> Create(ChapterDTO item) {
            Chapter chapter = await _database.ChapterRepository.Create(_mapper.Map<ChapterDTO, Chapter>(item));
            return _mapper.Map<Chapter, ChapterDTO>(chapter);
        }

        public async Task<ChapterDTO> Delete(string id) {
            Chapter chapter = await _database.ChapterRepository.Delete(id);
            return _mapper.Map<Chapter, ChapterDTO>(chapter);
        }

        public async Task<ChapterDTO> Update(ChapterDTO item) {
            Chapter chapter = await _database.ChapterRepository.Update(_mapper.Map<ChapterDTO, Chapter>(item));
            return _mapper.Map<Chapter, ChapterDTO>(chapter);
        }

        public IEnumerable<ChapterDTO> GetFanficChapters(string id) {
            IEnumerable<Chapter> chapters = _database.ChapterRepository.GetFanficChapters(id);
            return _mapper.Map<IEnumerable<Chapter>, IEnumerable<ChapterDTO>>(chapters);
        }

        public async Task<List<ChapterRatingDTO>> GetChapterRatingsAsync(string id) {
            List<ChapterRating> chapterRatings = (List<ChapterRating>)await _database.ChapterRepository.GetChapterRatingsAsync(id);
            return _mapper.Map<List<ChapterRating>, List<ChapterRatingDTO>>(chapterRatings);
        }
    }
}
