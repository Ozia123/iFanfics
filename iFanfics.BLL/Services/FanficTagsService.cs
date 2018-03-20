using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class FanficTagsService : IFanficTagsService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public FanficTagsService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<FanficTags> Query() {
            return _database.FanficTagsRepository.Query();
        }

        public async Task<FanficTagsDTO> GetById(string id) {
            FanficTags fanficTags = await _database.FanficTagsRepository.GetById(id);
            return _mapper.Map<FanficTags, FanficTagsDTO>(fanficTags);
        }

        public async Task<FanficTagsDTO> Create(FanficTagsDTO item) {
            FanficTags fanficTags = _mapper.Map<FanficTagsDTO, FanficTags>(item);
            fanficTags = await _database.FanficTagsRepository.Create(fanficTags);
            return _mapper.Map<FanficTags, FanficTagsDTO>(fanficTags);
        }

        public async Task<FanficTagsDTO> Delete(string id) {
            FanficTags fanficTags = await _database.FanficTagsRepository.Delete(id);
            return _mapper.Map<FanficTags, FanficTagsDTO>(fanficTags);
        }

        public async Task<FanficTagsDTO> Update(FanficTagsDTO item) {
            FanficTags fanficTags = _mapper.Map<FanficTagsDTO, FanficTags>(item);
            fanficTags = await _database.FanficTagsRepository.Update(fanficTags);
            return _mapper.Map<FanficTags, FanficTagsDTO>(fanficTags);
        }

        public IEnumerable<FanficTagsDTO> GetFanficTagsByFanficId(string id) {
            IEnumerable<FanficTags> fanficTags = _database.FanficTagsRepository.GetFanficTagsByFanficId(id);
            return _mapper.Map<IEnumerable<FanficTags>, IEnumerable<FanficTagsDTO>>(fanficTags);
        }

        public List<FanficTagsDTO> GetAll() {
            List<FanficTags> fanficTags = _database.FanficTagsRepository.GetAll();
            return _mapper.Map<List<FanficTags>, List<FanficTagsDTO>>(fanficTags);
        }
    }
}
