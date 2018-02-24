using AutoMapper;
using iFanfics.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using iFanfics.DAL.Entities;
using System.Linq;
using System;

namespace iFanfics.Web.Controllers {
    public class FanficController : Controller {
        private readonly IFanficService _fanficService;
        private readonly IFanficTagsService _fanficTagsService;
        private readonly IChapterService _chapterService;
        private readonly IGenreService _genreService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public FanficController(
            IFanficService fanficService,
            IFanficTagsService fanficTagsService,
            IChapterService chapterService,
            IGenreService genreService,
            ITagService tagService,
            SignInManager<ApplicationUser> authManager, 
            IUserService userService, 
            IMapper mapper) 
        {
            _fanficService = fanficService;
            _fanficTagsService = fanficTagsService;
            _chapterService = chapterService;
            _genreService = genreService;
            _tagService = tagService;
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/fanfic/{id}")]
        public async Task<IActionResult> GetAsync(string id) {
            FanficDTO fanfic = await _fanficService.GetById(id);

            if (fanfic == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelFromDTO(fanfic));
        }

        [HttpGet]
        [Route("api/user-fanfics/{id}")]
        public async Task<IActionResult> GetFanficsAsync(string id) {
            string username = id;
            ApplicationUser item = await _authenticationManager.UserManager.FindByNameAsync(username);
            if (item == null) {
                return NotFound();
            }

            List<FanficDTO> fanfics = (List<FanficDTO>)_fanficService.GetUserFanfics(item.Id);
            if (fanfics == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelsFromListDTO(fanfics));
        }

        [HttpGet]
        [Route("api/fanfics")]
        public async Task<IActionResult> GetAll(string id) {
            List<FanficDTO> fanfics = _fanficService.GetAll();
            if (fanfics == null) {
                return NotFound();
            }
            
            return Ok(await GetFanficModelsFromListDTO(fanfics));
        }

        private async Task<FanficModel> GetFanficModelFromDTO(FanficDTO fanfic) {
            return new FanficModel() {
                id = fanfic.Id,
                title = fanfic.Title,
                picture_url = fanfic.PictureURL,
                creation_date = fanfic.DateOfCreation.ToString(),
                description = fanfic.Description,
                genre = (await _genreService.GetById(fanfic.GenreId)).GenreName,
                author_username = (await _userService.GetUsernameById(fanfic.ApplicationUserId)),
                tags = GetFanficTags(await _fanficService.GetFanficTags(fanfic.Id))
            };
        }

        private async Task<IEnumerable<FanficModel>> GetFanficModelsFromListDTO(List<FanficDTO> fanfics) {
            List<FanficModel> fanficsModels = new List<FanficModel>();
            foreach (var fanfic in fanfics) {
                FanficModel model = new FanficModel() {
                    id = fanfic.Id,
                    title = fanfic.Title,
                    picture_url = fanfic.PictureURL,
                    creation_date = fanfic.DateOfCreation.ToString(),
                    description = fanfic.Description,
                    genre = (await _genreService.GetById(fanfic.GenreId)).GenreName,
                    author_username = (await _userService.GetUsernameById(fanfic.ApplicationUserId)),
                    tags = await GetFanficTags(fanfic.Id)
                };
                fanficsModels.Add(model);
            }
            fanficsModels.Sort((a, b) => b.creation_date.CompareTo(a.creation_date));
            return fanficsModels;
        }

        private async Task<List<string>> GetFanficTags(string fanficId) {
            List<FanficTagsDTO> items = _fanficTagsService.GetFanficTagsByFanficId(fanficId).ToList();
            if (items == null) {
                return new List<string>();
            }

            List<TagDTO> tags = new List<TagDTO>();
            foreach (var fanficTag in items) {
                tags.Add(await _tagService.GetById(fanficTag.TagId));
            }
            return GetFanficTags(tags);
        }

        private List<string> GetFanficTags(List<TagDTO> tags) {
            List<string> tagNames = new List<string>();
            foreach (var tag in tags) {
                tagNames.Add(tag.TagName);
            }
            return tagNames;
        }

        [HttpGet]
        [Route("api/fanfic/genres")]
        public IActionResult GetAllGenres() {
            List<GenreDTO> genres = _genreService.GetAll();
            if (genres == null) {
                return NotFound();
            }

            return Ok(genres);
        }

        [HttpPut]
        [Route("api/fanfic/edit")]
        public async Task<IActionResult> Put([FromBody]EditFanfic item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                if (item.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    FanficDTO fanfic = await _fanficService.Update(_mapper.Map<EditFanfic, FanficDTO>(item));
                    return Ok(fanfic);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("api/fanfic/delete/{id}")]
        public async Task<IActionResult> Delete([Required]string id) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                FanficDTO fanfic = await _fanficService.GetById(id);
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    fanfic = await _fanficService.Delete(id);
                    return Ok(fanfic);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/fanfic/create")]
        public async Task<IActionResult> Post([FromBody]CreateFanfic item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                if (_fanficService.CheckUniqueName(item.title)) {
                    FanficDTO fanfic = await GetFanficDTO(item);
                    fanfic = await _fanficService.Create(fanfic);
                    CreateTags(item.tags.ToList(), fanfic.Id);
                    return Ok(fanfic);
                }
                return BadRequest("title is already exists");
            }
            return BadRequest(ModelState);
        }

        private async Task<FanficDTO> GetFanficDTO(CreateFanfic item) {
            ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
            return new FanficDTO() {
                Title = item.title,
                Description = item.description,
                ApplicationUserId = user.Id,
                PictureURL = item.pictureUrl,
                GenreId = _genreService.GetByName(item.genre).Id,
                DateOfCreation = DateTime.Now,
                LastModifyingDate = DateTime.Now
            };
        }

        private void CreateTags(List<string> tags, string fanficId) {
            if (tags == null) {
                return;
            }
            System.IO.File.AppendAllLines("D:/info.txt", tags);
            foreach (var tag in tags) {
                CreateTag(tag, fanficId);
            }
        }

        private async void CreateTag(string tagName, string fanficId) {
            TagDTO tag = _tagService.GetTagByName(tagName);
            if (tag == null) {
                tag = await _tagService.Create(new TagDTO() { TagName = tagName, Uses = 1 });
                return;
            }
            await _fanficTagsService.Create(new FanficTagsDTO() { TagId = tag.Id, FanficId = fanficId });
            return;
        }

        [HttpPost]
        [Route("api/fanfic/createchapter")]
        public async Task<IActionResult> CreateChapterForFanfic([FromBody]CreateChapter item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(item.FanficId);

                if (fanfic == null) {
                    return NotFound();
                }
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    ChapterDTO newChapter = _mapper.Map<CreateChapter, ChapterDTO>(item);
                    newChapter.ChapterNumber = (await _fanficService.GetChapters(fanfic.Id)).Count + 1;
                    newChapter = await _chapterService.Create(newChapter);
                    return Ok(newChapter);
                }
            }

            return BadRequest(ModelState);
        }
    }
}