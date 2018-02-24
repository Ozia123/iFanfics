using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class ChapterRatingService : IChapterRatingService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public ChapterRatingService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChapterRatingDTO> GetById(string id) {
            ChapterRating chapterRating = await _database.ChapterRatingRepository.GetById(id);
            return _mapper.Map<ChapterRating, ChapterRatingDTO>(chapterRating);
        }

        public bool CheckForGivenRating(string userId, string chapterId) {
            return _database.ChapterRatingRepository.CheckForGivenRating(userId, chapterId);
        }

        public async Task<ChapterRatingDTO> Create(ChapterRatingDTO item) {
            ChapterRating chapterRating = await _database.ChapterRatingRepository.Create(_mapper.Map<ChapterRatingDTO, ChapterRating>(item));
            return _mapper.Map<ChapterRating, ChapterRatingDTO>(chapterRating);
        }

        public async Task<ChapterRatingDTO> Delete(string id) {
            ChapterRating chapterRating = await _database.ChapterRatingRepository.Delete(id);
            return _mapper.Map<ChapterRating, ChapterRatingDTO>(chapterRating);
        }

        public async Task<ChapterRatingDTO> Update(ChapterRatingDTO item) {
            ChapterRating chapterRating = await _database.ChapterRatingRepository.Update(_mapper.Map<ChapterRatingDTO, ChapterRating>(item));
            return _mapper.Map<ChapterRating, ChapterRatingDTO>(chapterRating);
        }
    }
}
