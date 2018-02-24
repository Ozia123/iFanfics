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
    public class UserController : Controller {
        private readonly IFanficService _fanficService;
        private readonly IChapterService _chapterService;
        private readonly IGenreService _genreService;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public UserController(
            IFanficService fanficService,
            IChapterService chapterService,
            IGenreService genreService,
            SignInManager<ApplicationUser> authManager,
            IUserService userService,
            IMapper mapper) {
            _fanficService = fanficService;
            _chapterService = chapterService;
            _genreService = genreService;
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public async Task<IActionResult> GetAsync([Required]string id) {
            string username = id;
            ApplicationUser item = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
            if (item == null) {
                return NotFound();
            }

            CurrentUser currentUser = _mapper.Map<UserDTO, CurrentUser>(await _userService.GetUserById(item.Id));
            CurrentUser user = new CurrentUser {
                UserName = currentUser.UserName,
                roles = await _authenticationManager.UserManager.GetRolesAsync(item),
                isAuntificated = User.Identity.IsAuthenticated,
                PictureURL = currentUser.PictureURL,
            };

            return Ok(user);
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> GetAll(string id) {
            List<string> usersIds = _userService.GetAllUsersIds();
            if (usersIds == null) {
                return NotFound();
            }

            return Ok(await GetEnumerableCurrentUsersModelsByIds(usersIds));
        }

        private async Task<IEnumerable<CurrentUser>> GetEnumerableCurrentUsersModelsByIds(List<string> usersIds) {
            List<CurrentUser> users = new List<CurrentUser>();
            foreach (var id in usersIds) {
                users.Add(await GetCurrentUserModelById(id));
            }
            return users;
        }

        private async Task<CurrentUser> GetCurrentUserModelById(string id) {
            ApplicationUser item = await _authenticationManager.UserManager.FindByIdAsync(id);

            CurrentUser currentUser = _mapper.Map<UserDTO, CurrentUser>(await _userService.GetUserById(item.Id));
            CurrentUser user = new CurrentUser {
                UserName = currentUser.UserName,
                roles = await _authenticationManager.UserManager.GetRolesAsync(item),
                isAuntificated = User.Identity.IsAuthenticated,
                PictureURL = currentUser.PictureURL,
            };

            return user;
        }

        
    }
}