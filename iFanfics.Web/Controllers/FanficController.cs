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

namespace iFanfics.Web.Controllers {
    public class FanficController : Controller {
        private readonly IFanficService _fanficService;
        private readonly IChapterService _chapterService;
        private readonly IGenreService _genreService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public FanficController(
            IFanficService fanficService, 
            IChapterService chapterService,
            IGenreService genreService, 
            SignInManager<ApplicationUser> authManager, 
            IUserService userService, 
            IMapper mapper) 
        {
            _fanficService = fanficService;
            _chapterService = chapterService;
            _genreService = genreService;
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
                    tags = GetFanficTags(await _fanficService.GetFanficTags(fanfic.Id))
                };
                fanficsModels.Add(model);
            }
            return fanficsModels;
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
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                FanficDTO newFanfic = _mapper.Map<CreateFanfic, FanficDTO>(item);
                newFanfic.ApplicationUserId = user.Id;
                FanficDTO fanific = await _fanficService.Create(newFanfic);
                return Ok(fanific);
            }

            return BadRequest(ModelState);
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