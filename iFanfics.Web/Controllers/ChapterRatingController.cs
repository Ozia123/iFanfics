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
    public class ChapterRatingController : Controller {
        private readonly IFanficService _fanficService;
        private readonly IChapterService _chapterService;
        private readonly IChapterRatingService _chapterRatingService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public ChapterRatingController(
            IFanficService fanficService,
            IChapterService chapterService,
            SignInManager<ApplicationUser> authManager,
            IChapterRatingService chapterRatingService,
            IUserService userService,
            IMapper mapper) 
        {
            _fanficService = fanficService;
            _chapterService = chapterService;
            _chapterRatingService = chapterRatingService;
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        private async Task<ChapterRatingModel> GetChapterRatingModelFromDTO(ChapterRatingDTO rating) {
            UserDTO user = await _userService.GetUserById(rating.ApplicationUserId);
            return new ChapterRatingModel {
                username = user.Username,
                givenRating = rating.GivenRating,
                chapterId = rating.ChapterId,
                id = rating.Id
            };
        }

        private async Task<ChapterRatingDTO> GetChapterRatingDTOFromModel(ChapterRatingModel rating, string userId, string chapterId) {
            UserDTO user = await _userService.GetUserById(userId);
            return new ChapterRatingDTO {
                ChapterId = chapterId,
                ApplicationUserId = userId,
                GivenRating = rating.givenRating
            };
        }

        private async Task<IEnumerable<ChapterRatingModel>> GetChapterRatingModelsFromDTOs(List<ChapterRatingDTO> ratings) {
            List<ChapterRatingModel> ratingModels = new List<ChapterRatingModel>();
            foreach (var rating in ratings) {
                ratingModels.Add(await GetChapterRatingModelFromDTO(rating));
            }
            return ratingModels;
        }

        [HttpGet]
        [Route("api/chapter/ratings/{id}")]
        public async Task<IActionResult> GetChapterRatings([Required]string id) {
            if (ModelState.IsValid) {
                IEnumerable<ChapterRatingDTO> ratings = _chapterRatingService.GetChapterRatings(id);
                if (ratings == null) {
                    return Ok(new List<CommentRatingModel>());
                }
                return Ok(await GetChapterRatingModelsFromDTOs(ratings.ToList()));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/chapter/rating/create/{id}")]
        public async Task<IActionResult> CreateChapterRating([Required]string id, [FromBody]ChapterRatingModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                ChapterRatingDTO chapterRating = await GetChapterRatingDTOFromModel(item, user.Id, id);
                chapterRating = await _chapterRatingService.Create(chapterRating);
                return Ok(await GetChapterRatingModelFromDTO(chapterRating));
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("api/chapter/rating/delete/{id}")]
        public async Task<IActionResult> DeleteChapterRating([Required]string id) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                ChapterRatingDTO chapterRating = await _chapterRatingService.GetById(id);

                if (chapterRating == null) {
                    return NotFound();
                }
                if (chapterRating.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    ChapterRatingDTO deletedChapterRating = await _chapterRatingService.Delete(id);
                    return Ok(await GetChapterRatingModelFromDTO(deletedChapterRating));
                }
            }
            return BadRequest(ModelState);
        }
    }
}
