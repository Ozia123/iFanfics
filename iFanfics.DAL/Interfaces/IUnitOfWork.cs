using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace iFanfics.DAL.Interfaces {
    public interface IUnitOfWork {
        ApplicationContext Context { get; }
        UserManager<ApplicationUser> UserManager { get; }
        SignInManager<ApplicationUser> SignInManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        IProfileRepository ProfileRepository { get; }
        IFanficRepository FanficRepository { get; }
        IChapterRepository ChapterRepository { get; }
        IChapterRatingRepository ChapterRatingRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICommentRatingRepository CommentRatingRepository { get; }
        IGenreRepository GenreRepository { get; }
        ITagRepository TagRepository { get; }

        Task SaveAsync();
    }
}