using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace iFanfics.BLL.Services {
    public class CommentRatingService : ICommentRatingService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CommentRatingService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<CommentRating> Query() {
            return _database.CommentRatingRepository.Query();
        }

        public async Task<CommentRatingDTO> GetById(string id) {
            CommentRating commentRating = await _database.CommentRatingRepository.GetById(id);
            return _mapper.Map<CommentRating, CommentRatingDTO>(commentRating);
        }

        public bool CheckForGivenRating(string userId, string commentId) {
            return _database.CommentRatingRepository.CheckForGivenRating(userId, commentId);
        }

        public async Task<CommentRatingDTO> Create(CommentRatingDTO item) {
            CommentRating commentRating = await _database.CommentRatingRepository.Create(_mapper.Map<CommentRatingDTO, CommentRating>(item));
            return _mapper.Map<CommentRating, CommentRatingDTO>(commentRating);
        }

        public async Task<CommentRatingDTO> Delete(string id) {
            CommentRating commentRating = await _database.CommentRatingRepository.Delete(id);
            return _mapper.Map<CommentRating, CommentRatingDTO>(commentRating);
        }

        public async Task<CommentRatingDTO> Update(CommentRatingDTO item) {
            CommentRating commentRating = await _database.CommentRatingRepository.Update(_mapper.Map<CommentRatingDTO, CommentRating>(item));
            return _mapper.Map<CommentRating, CommentRatingDTO>(commentRating);
        }

        public IEnumerable<CommentRatingDTO> GetCommentRatings(string id) {
            IEnumerable<CommentRating> ratings = _database.CommentRatingRepository.GetCommentRatins(id);
            return _mapper.Map<IEnumerable<CommentRating>, IEnumerable<CommentRatingDTO>>(ratings);
        }
    }
}
