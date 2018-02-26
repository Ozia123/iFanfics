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
    public class CommentController : Controller {
        private readonly IFanficService _fanficService;
        private readonly ICommentService _commentService;
        private readonly ICommentRatingService _commentRatingService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public CommentController(
            IFanficService fanficService,
            ICommentService commentService,
            ICommentRatingService commentRatingService,
            SignInManager<ApplicationUser> authManager,
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

        private async Task<CommentModel> GetCommentModelFromDTO(CommentDTO comment) {
            UserDTO user = await _userService.GetUserById(comment.ApplicationUserId);
            return new CommentModel {
                username = user.Username,
                pictureUrl = user.PictureURL,
                comment = comment.CommentText,
                id = comment.Id
            };
        }

        private async Task<CommentDTO> GetCommentDTOFromModel(CommentModel comment, string userId, string fanficId) {
            UserDTO user = await _userService.GetUserById(userId);
            return new CommentDTO {
                FanficId = fanficId,
                ApplicationUserId = userId,
                CommentText = comment.comment
            };
        }

        private async Task<IEnumerable<CommentModel>> GetCommentModelsFromDTOs(List<CommentDTO> comments) {
            List<CommentModel> commentsModels = new List<CommentModel>();
            foreach (var comment in comments) {
                commentsModels.Add(await GetCommentModelFromDTO(comment));
            }
            return commentsModels;
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

        [HttpGet]
        [Route("api/comments/{id}")]
        public async Task<IActionResult> GetFanficComments([Required]string id) {
            if (ModelState.IsValid) {
                IEnumerable<CommentDTO> comments = _commentService.GetFanficComments(id);
                if (comments == null) {
                    return Ok(new List<CommentModel>());
                }
                return Ok(await GetCommentModelsFromDTOs(comments.ToList()));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/comment/create/{id}")]
        public async Task<IActionResult> CreateCommentForFanfic([Required]string id, [FromBody]CommentModel item) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                CommentDTO newComment = await GetCommentDTOFromModel(item, user.Id, id);
                newComment = await _commentService.Create(newComment);
                return Ok(await GetCommentModelFromDTO(newComment));
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("api/comment/delete/{id}")]
        public async Task<IActionResult> DeleteCommentFromFanfic([Required]string id) {
            if (ModelState.IsValid && User.Identity.IsAuthenticated) {
                ApplicationUser user = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                CommentDTO comment = await _commentService.GetById(id);

                if (comment == null) {
                    return NotFound();
                }
                if (comment.ApplicationUserId == user.Id || await _authenticationManager.UserManager.IsInRoleAsync(user, "Admin")) {
                    await DeleteCommentRatings(id);
                    CommentDTO deletedComment = await _commentService.Delete(id);
                    return Ok(await GetCommentModelFromDTO(deletedComment));
                }
            }
            return BadRequest(ModelState);
        }
    }
}
