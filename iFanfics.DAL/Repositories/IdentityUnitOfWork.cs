using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iFanfics.DAL.Repositories {
    public class IdentityUnitOfWork : IUnitOfWork {
        private readonly ApplicationContext _database;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IProfileRepository _profileRepository;
        private readonly IFanficRepository _fanficRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IChapterRatingRepository _chapterRatingRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentRatingRepository _commentRatingRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ITagRepository _tagRepository;

        public IdentityUnitOfWork(
                ApplicationContext database, 
                UserManager<ApplicationUser> userManager, 
                RoleManager<IdentityRole> roleManager, 
                SignInManager<ApplicationUser> signInManager) {
            _database = database;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            _profileRepository = new ProfileRepository(_database);
            _fanficRepository = new FanficRepository(_database);
            _chapterRepository = new ChapterRepository(_database);
            _chapterRatingRepository = new ChapterRatingRepository(_database);
            _commentRepository = new CommentRepository(_database);
            _commentRatingRepository = new CommentRatingRepository(_database);
            _genreRepository = new GenreRepository(_database);
            _tagRepository = new TagRepository(_database);
        }

        public ApplicationContext Context => _database;

        public UserManager<ApplicationUser> UserManager => _userManager;

        public SignInManager<ApplicationUser> SignInManager => _signInManager;

        public RoleManager<IdentityRole> RoleManager => _roleManager;

        public IProfileRepository ProfileRepository => _profileRepository;

        public IFanficRepository FanficRepository => _fanficRepository;

        public IChapterRepository ChapterRepository => _chapterRepository;

        public IChapterRatingRepository ChapterRatingRepository => _chapterRatingRepository;

        public ICommentRepository CommentRepository => _commentRepository;

        public ICommentRatingRepository CommentRatingRepository => _commentRatingRepository;

        public IGenreRepository GenreRepository => _genreRepository;

        public ITagRepository TagRepository => _tagRepository;

        public async Task SaveAsync() {
            await _database.SaveChangesAsync();
        }
    }
}
