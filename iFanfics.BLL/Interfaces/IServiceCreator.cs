namespace iFanfics.BLL.Interfaces {
    public interface IServiceCreator {
        IUserService CreateUserService();
        IFanficService CreateFanficService();
        IChapterService CreateChapterService();
        IChapterRatingService CreateChapterRatingService();
        ICommentService CreateCommentService();
        ICommentRatingService CreateCommentRatingService();
        IGenreService CreateGenreService();
        ITagService CreateTagService();
    }
}