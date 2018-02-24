using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;
using System;

namespace iFanfics.BLL.Services {
    public class FanficService : IFanficService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public FanficService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Fanfic> Query() {
            return _database.FanficRepository.Query();
        }

        public bool CheckUniqueName(string value) {
            return _database.FanficRepository.CheckUniqueTitle(value);
        }

        public IEnumerable<FanficDTO> GetUserFanfics(string userId) {
            IEnumerable<Fanfic> fanfics = _database.FanficRepository.GetUserFanfics(userId);
            return _mapper.Map<IEnumerable<Fanfic>, IEnumerable<FanficDTO>>(fanfics);
        }

        public async Task<FanficDTO> Create(FanficDTO item) {
            Fanfic fanfic = _mapper.Map<FanficDTO, Fanfic>(item);
            fanfic.DateOfCreation = DateTime.Now;
            fanfic.LastModifyingDate = DateTime.Now;
            fanfic = await _database.FanficRepository.Create(fanfic);
            return _mapper.Map<Fanfic, FanficDTO>(fanfic);
        }

        public async Task<FanficDTO> Delete(string id) {
            Fanfic fanfic = await _database.FanficRepository.Delete(id);
            return _mapper.Map<Fanfic, FanficDTO>(fanfic);
        }

        public async Task<FanficDTO> Update(FanficDTO item) {
            Fanfic fanfic = _mapper.Map<FanficDTO, Fanfic>(item);
            string oldGenreId = fanfic.GenreId;
            fanfic.LastModifyingDate = DateTime.Now;
            fanfic = await _database.FanficRepository.Update(fanfic);
            if (fanfic.GenreId != oldGenreId) {
                // ToQueueElasticUpdate
            }

            return _mapper.Map<Fanfic, FanficDTO>(fanfic);
        }

        public async Task<FanficDTO> GetById(string id) {
            Fanfic fanfic = await _database.FanficRepository.GetById(id);
            return _mapper.Map<Fanfic, FanficDTO>(fanfic);
        }

        public List<FanficDTO> GetAll() {
            List<Fanfic> fanfics = _database.FanficRepository.Query().ToList();
            return _mapper.Map<List<Fanfic>, List<FanficDTO>>(fanfics);
        }

        public async Task<List<ChapterDTO>> GetChapters(string id) {
            List<Chapter> chapters = (List<Chapter>)await _database.FanficRepository.GetChaptersAsync(id);
            return _mapper.Map<List<Chapter>, List<ChapterDTO>>(chapters);
        }

        public async Task<List<CommentDTO>> GetComments(string id) {
            List<Comment> comments = (List<Comment>)await _database.FanficRepository.GetCommentsAsync(id);
            return _mapper.Map<List<Comment>, List<CommentDTO>>(comments);
        }

        public async Task<List<TagDTO>> GetFanficTags(string id) {
            List<Tag> tags = (List<Tag>)await _database.FanficRepository.GetFanficTagsAsync(id);
            return _mapper.Map<List<Tag>, List<TagDTO>>(tags);
        }
    }
}