﻿using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class CommentService : ICommentService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Comment> Query() {
            return _database.CommentRepository.Query();
        }

        public async Task<CommentDTO> GetById(string id) {
            Comment comment = await _database.CommentRepository.GetById(id);
            return _mapper.Map<Comment, CommentDTO>(comment);
        }

        public async Task<CommentDTO> Create(CommentDTO item) {
            Comment comment = await _database.CommentRepository.Create(_mapper.Map<CommentDTO, Comment>(item));
            return _mapper.Map<Comment, CommentDTO>(comment);
        }

        public async Task<CommentDTO> Delete(string id) {
            Comment comment = await _database.CommentRepository.Delete(id);
            return _mapper.Map<Comment, CommentDTO>(comment);
        }

        public async Task<CommentDTO> Update(CommentDTO item) {
            Comment comment = await _database.CommentRepository.Update(_mapper.Map<CommentDTO, Comment>(item));
            return _mapper.Map<Comment, CommentDTO>(comment);
        }

        public async Task<List<CommentRatingDTO>> GetCommentRatingsAsync(string id) {
            List<CommentRating> commentRatings = (List<CommentRating>)await _database.CommentRepository.GetCommentRatingsAsync(id);
            return _mapper.Map<List<CommentRating>, List<CommentRatingDTO>>(commentRatings);
        }
    }
}