using iFanfics.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iFanfics.DAL.EF {
    public class ApplicationContext : IdentityDbContext<ApplicationUser> {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Fanfic> Fanfics { get; set; }

        public DbSet<FanficTags> FanficsTags { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<ChapterRating> ChaptersRating { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentRating> CommentsRating { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}
