using Microsoft.AspNetCore.Identity;
using iFanfics.DAL.Repositories;
using iFanfics.BLL.Interfaces;
using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using AutoMapper;

namespace iFanfics.BLL.Services {
    public class ServiceCreator : IServiceCreator {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public ServiceCreator(
                ApplicationContext context,
                IMapper mapper,
                UserManager<ApplicationUser> userManager = null,
                RoleManager<IdentityRole> roleManager = null,
                SignInManager<ApplicationUser> signInManager = null
            ) {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IChapterService CreateChapterService() {
            return new ChapterService(CreateUnitOfWork(), _mapper);
        }

        public ICommentService CreateCommentService() {
            return new CommentService(CreateUnitOfWork(), _mapper);
        }

        public IFanficService CreateFanficService() {
            return new FanficService(CreateUnitOfWork(), _mapper);
        }

        public IGenreService CreateGenreService() {
            return new GenreService(CreateUnitOfWork(), _mapper);
        }

        public ITagService CreateTagService() {
            return new TagService(CreateUnitOfWork(), _mapper);
        }

        public IUserService CreateUserService() {
            return new UserService(CreateUnitOfWork(), _mapper);
        }

        public IdentityUnitOfWork CreateUnitOfWork() {
            return new IdentityUnitOfWork(_context, _userManager, _roleManager, _signInManager);
        }

        public IChapterRatingService CreateChapterRatingService() {
            return new ChapterRatingService(CreateUnitOfWork(), _mapper);
        }

        public ICommentRatingService CreateCommentRatingService() {
            return new CommentRatingService(CreateUnitOfWork(), _mapper);
        }

        public IFanficTagsService CreateFanficTagsService() {
            return new FanficTagsService(CreateUnitOfWork(), _mapper);
        }
    }
}
