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
        private readonly ICommentService _commentService;
        private readonly ICommentRatingService _commentRatingService;
        private readonly IGenreService _genreService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public FanficController(
            IFanficService fanficService,
            IFanficTagsService fanficTagsService,
            IChapterService chapterService,
            ICommentService commentService,
            ICommentRatingService commentRatingService,
            IGenreService genreService,
            ITagService tagService,
            SignInManager<ApplicationUser> authManager,
            IUserService userService,
            IMapper mapper) 
        {
            _fanficService = fanficService;
            _fanficTagsService = fanficTagsService;
            _chapterService = chapterService;
            _commentService = commentService;
            _commentRatingService = commentRatingService;
            _genreService = genreService;
            _tagService = tagService;
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/fanfic/comment-count/{id}")]
        public IActionResult GetCountOfComments([Required]string id) {
            IEnumerable<CommentDTO> comments = _commentService.GetFanficComments(id) ?? new List<CommentDTO>();
            return Ok(new CommentsCountModel { count = comments.Count() });
        }

        [HttpGet]
        [Route("api/fanfic/{id}")]
        public async Task<IActionResult> GetAsync([Required]string id) {
            FanficDTO fanfic = await _fanficService.GetById(id);

            if (fanfic == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelFromDTO(fanfic));
        }

        [HttpGet]
        [Route("api/user-fanfics/{id}")]
        public async Task<IActionResult> GetFanficsAsync([Required]string id) {
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
        [Route("api/fanfics/get-by-tag/{id}")]
        public async Task<IActionResult> GetFanficsByTag([Required]string id) {
            TagDTO tag = _tagService.GetTagByName(id);
            IEnumerable<FanficTagsDTO> fanficTags = _fanficTagsService.GetAll();
            IEnumerable<FanficDTO> fanfics = _fanficService.GetAll().Where(x => fanficTags.Any(y => y.TagId.Equals(tag.Id) && y.FanficId.Equals(x.Id)));
            if (fanfics == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelsFromListDTO(fanfics.ToList()));
        }

        [HttpGet]
        [Route("api/fanfics/get-by-genre/{id}")]
        public async Task<IActionResult> GetFanficsByGenre([Required]string id) {
            GenreDTO genre = _genreService.GetByName(id);
            IEnumerable<FanficDTO> fanfics = _fanficService.GetAll().Where(x => x.GenreId.Equals(genre.Id));
            if (fanfics == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelsFromListDTO(fanfics.ToList()));
        }

        [HttpGet]
        [Route("api/fanfics/get-by-query/{id}")]
        public async Task<IActionResult> GetFanficsByQuery([Required]string id) {
            IEnumerable<FanficDTO> fanfics = _fanficService.GetAll().Where(x => x.Title.Contains(id) || x.Description.Contains(id));
            if (fanfics == null) {
                return NotFound();
            }

            return Ok(await GetFanficModelsFromListDTO(fanfics.ToList()));
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
        public async Task<IActionResult> Put([FromBody]FanficModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO fanfic = await _fanficService.GetById(item.id);
                if (fanfic.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    fanfic = GetFanficDTOForUpdating(fanfic, item);
                    await DeleteFanficTagsByFanficId(item.id);
                    await CreateTags(item.tags.ToList(), item.id);
                    await _fanficService.Update(fanfic);
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
                    await DeleteFanficTagsByFanficId(id);
                    await DeleteFanficChapters(id);
                    await DeleteFanficComments(id);

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
                    await CreateTags(item.tags.ToList(), fanfic.Id);
                    return Ok(fanfic.Id);
                }
                return BadRequest("title is already exists");
            }
            return BadRequest(ModelState);
        }

        private async Task<FanficModel> GetFanficModelFromDTO(FanficDTO fanfic) {
            UserDTO user = await _userService.GetUserById(fanfic.ApplicationUserId);
            return new FanficModel() {
                id = fanfic.Id,
                title = fanfic.Title,
                picture_url = fanfic.PictureURL,
                creation_date = fanfic.DateOfCreation.ToString(),
                last_modifying_date = fanfic.LastModifyingDate.ToString(),
                description = fanfic.Description,
                genre = (await _genreService.GetById(fanfic.GenreId)).GenreName,
                author_username = user.Username,
                author_picture_url = user.PictureURL,
                tags = await GetFanficTags(fanfic.Id)
            };
        }

        private FanficDTO GetFanficDTOForUpdating(FanficDTO fanfic, FanficModel item) {
            fanfic.Id = null;
            fanfic.Title = item.title;
            fanfic.Description = item.description;
            fanfic.PictureURL = item.picture_url;
            fanfic.GenreId = _genreService.GetByName(item.genre).Id;
            fanfic.DateOfCreation = DateTime.Parse(item.creation_date);
            fanfic.LastModifyingDate = DateTime.Now;
            return fanfic;
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

        private async Task<IEnumerable<FanficModel>> GetFanficModelsFromListDTO(List<FanficDTO> fanfics) {
            fanfics.Sort((a, b) => b.LastModifyingDate.CompareTo(a.LastModifyingDate));
            List<FanficModel> fanficsModels = new List<FanficModel>();
            foreach (var fanfic in fanfics) {
                FanficModel model = await GetFanficModelFromDTO(fanfic);
                fanficsModels.Add(model);
            }
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

        private async Task CreateTags(List<string> tags, string fanficId) {
            if (tags == null) {
                return;
            }
            foreach (var tag in tags) {
                await CreateTag(tag, fanficId);
            }
            return;
        }

        private async Task CreateTag(string tagName, string fanficId) {
            TagDTO tag = _tagService.GetTagByName(tagName);
            if (tag == null) {
                tag = await _tagService.Create(new TagDTO() { TagName = tagName, Uses = 1 });
            }
            await _fanficTagsService.Create(new FanficTagsDTO() { TagId = tag.Id, FanficId = fanficId });
            return;
        }

        private async Task DeleteFanficTagsByFanficId(string id) {
            List<FanficTagsDTO> tags = _fanficTagsService.GetFanficTagsByFanficId(id).ToList();
            if (tags == null) {
                return;
            }

            foreach (var tag in tags) {
                await _fanficTagsService.Delete(tag.Id);
            }
        }

        private async Task<List<ChapterDTO>> DeleteFanficChapters(string id) {
            List<ChapterDTO> chapters = _chapterService.GetFanficChapters(id).ToList();
            if (chapters == null) {
                return new List<ChapterDTO>();
            }

            foreach (var chapter in chapters) {
                await _chapterService.Delete(chapter.Id);
            }
            return chapters;
        }

        private async Task<List<CommentDTO>> DeleteFanficComments(string id) {
            List<CommentDTO> comments = _commentService.GetFanficComments(id).ToList();
            if (comments == null) {
                return new List<CommentDTO>();
            }

            foreach (var comment in comments) {
                await DeleteCommentRatings(comment.Id);
                await _commentService.Delete(comment.Id);
            }
            return comments;
        }

        private async Task DeleteCommentRatings(string id) {
            IEnumerable<CommentRatingDTO> ratings = _commentRatingService.GetCommentRatings(id);
            if (ratings == null) {
                return;
            }
            foreach (var rating in ratings) {
                await _commentRatingService.Delete(rating.Id);
            }
        }
    }
}