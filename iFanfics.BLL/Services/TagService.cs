using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iFanfics.DAL.Interfaces;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class TagService : ITagService {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper) {
            _database = unitOfWork;
            _mapper = mapper;
        }

        public IQueryable<Tag> Query() {
            return _database.TagRepository.Query();
        }

        public async Task<TagDTO> GetById(string id) {
            Tag tag = await _database.TagRepository.GetById(id);
            return _mapper.Map<Tag, TagDTO>(tag);
        }

        public IEnumerable<TagDTO> AddRange(List<TagDTO> items) {
            IEnumerable<Tag> tags = _database.TagRepository.AddRange(_mapper.Map<List<TagDTO>, List<Tag>>(items));
            return _mapper.Map<IEnumerable<Tag>, IEnumerable<TagDTO>>(tags);
        }

        public bool CheckForExistingTag(string value) {
            return _database.TagRepository.CheckForExistingTag(value);
        }

        public TagDTO GetTagByName(string tagName) {
            Tag tag = _database.TagRepository.GetByName(tagName);
            return _mapper.Map<Tag, TagDTO>(tag);
        }

        public async Task<TagDTO> Create(TagDTO item) {
            Tag tag = await _database.TagRepository.Create(_mapper.Map<TagDTO, Tag>(item));
            return _mapper.Map<Tag, TagDTO>(tag);
        }

        public async Task<TagDTO> Delete(string id) {
            Tag tag = await _database.TagRepository.Delete(id);
            return _mapper.Map<Tag, TagDTO>(tag);
        }

        public async Task<TagDTO> Update(TagDTO item) {
            Tag tag = await _database.TagRepository.Update(_mapper.Map<TagDTO, Tag>(item));
            return _mapper.Map<Tag, TagDTO>(tag);
        }

        public List<TagDTO> GetAll() {
            List<Tag> tags = _database.TagRepository.Query().ToList();
            return _mapper.Map<List<Tag>, List<TagDTO>>(tags);
        }
    }
}
