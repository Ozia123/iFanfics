using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using iFanfics.BLL.DTO;
using iFanfics.BLL.Infrastucture;
using iFanfics.BLL.Interfaces;
using iFanfics.DAL.Entities;
using iFanfics.Web.Models;

namespace iFanfics.Web.Controllers {
    public class AuthController : Controller {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _authenticationManager;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, SignInManager<ApplicationUser> authManager, IMapper mapper) {
            _userService = userService;
            _authenticationManager = authManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/auth/currentuser")]
        public async Task<CurrentUser> GetCurrentUser() {
            if (User.Identity.IsAuthenticated) {
                ApplicationUser item = await _authenticationManager.UserManager.FindByNameAsync(User.Identity.Name);
                CurrentUser currentUser = _mapper.Map<UserDTO, CurrentUser>(await _userService.GetUserById(item.Id));

                CurrentUser user = new CurrentUser {
                    UserName = currentUser.UserName,
                    roles = await _authenticationManager.UserManager.GetRolesAsync(item),
                    isAuntificated = User.Identity.IsAuthenticated,
                    PictureURL = currentUser.PictureURL,
                };
                return user;
            }
            return new CurrentUser();
        }

        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody]LoginModel item) {
            if (ModelState.IsValid) {
                var result = await _authenticationManager.PasswordSignInAsync(item.Username, item.Password, true, false);
                if (result.Succeeded) {
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("api/auth/logout")]
        public async Task<IActionResult> Logout() {
            await _authenticationManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("api/auth/register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model) {
            if (ModelState.IsValid) {
                UserDTO userDto = _mapper.Map<RegisterModel, UserDTO>(model);
                userDto.Role = "User";
                OperationDetails operationDetails = await _userService.Create(userDto);
                if (operationDetails.Succedeed) {
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
