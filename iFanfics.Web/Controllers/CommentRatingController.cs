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
    public class CommentRatingController : Controller {
        private readonly IFanficService _fanficService;
        private readonly ICommentService _commentService;
        private readonly ICommentRatingService _commentRatingService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public CommentRatingController(
            IFanficService fanficService,
            ICommentService commentService,
            SignInManager<ApplicationUser> authManager,
            ICommentRatingService commentRatingService,
            IUserService userService,
            IMapper mapper) 
        {
            _fanficService = fanficService;
            _commentService = commentService;
            _commentRatingService = commentRatingService;
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        private async Task<CommentRatingModel> GetCommentRatingModelFromDTO(CommentRatingDTO rating) {
            UserDTO user = await _userService.GetUserById(rating.ApplicationUserId);
            return new CommentRatingModel {
                username = user.Username,
                givenRating = rating.GivenRating,
                commentId = rating.CommentId,
                id = rating.Id
            };
        }

        private async Task<CommentRatingDTO> GetCommentRatingDTOFromModel(CommentRatingModel rating, string userId, string commentId) {
            UserDTO user = await _userService.GetUserById(userId);
            return new CommentRatingDTO {
                CommentId = commentId,
                ApplicationUserId = userId,
                GivenRating = rating.givenRating
            };
        }

        private async Task<IEnumerable<CommentRatingModel>> GetCommentRatingModelsFromDTOs(List<CommentRatingDTO> ratings) {
            List<CommentRatingModel> ratingModels = new List<CommentRatingModel>();
            foreach (var rating in ratings) {
                ratingModels.Add(await GetCommentRatingModelFromDTO(rating));
            }
            return ratingModels;
        }

        [HttpGet]
        [Route("api/comment/ratings/{id}")]
        public async Task<IActionResult> GetCommentRatings([Required]string id) {
            if (ModelState.IsValid) {
                IEnumerable<CommentRatingDTO> ratings = _commentRatingService.GetCommentRatings(id);
                if (ratings == null) {
                    return Ok(new List<CommentRatingModel>());
                }
                return Ok(await GetCommentRatingModelsFromDTOs(ratings.ToList()));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/comment/rating/create/{id}")]
        public async Task<IActionResult> CreateCommentRating([Required]string id, [FromBody]CommentRatingModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                CommentRatingDTO commentRating = await GetCommentRatingDTOFromModel(item, user.Id, id);
                commentRating = await _commentRatingService.Create(commentRating);
                return Ok(await GetCommentRatingModelFromDTO(commentRating));
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("api/comment/rating/delete/{id}")]
        public async Task<IActionResult> DeleteCommentRating([Required]string id) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                CommentRatingDTO commentRating = await _commentRatingService.GetById(id);

                if (commentRating == null) {
                    return NotFound();
                }
                if (commentRating.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    CommentRatingDTO deletedCommentRating = await _commentRatingService.Delete(id);
                    return Ok(await GetCommentRatingModelFromDTO(deletedCommentRating));
                }
            }
            return BadRequest(ModelState);
        }
    }
}
