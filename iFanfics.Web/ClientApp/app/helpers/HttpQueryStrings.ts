export class HttpQueryStrings {
    public static LoginUser: string = '/api/auth/login';
    public static RegiserUser: string = 'api/auth/register';
    public static LogoutUser: string = 'api/auth/logout';
    public static CurrentUser: string = 'api/auth/currentuser';

    public static GetAllFanfics: string = 'api/fanfics';
    public static GetAllFanficsByQuery: string = 'api/fanfics/get-by-query/';
    public static GetAllFanficsByGenre: string = 'api/fanfics/get-by-genre/';
    public static GetAllFanficsByTag: string = 'api/fanfics/get-by-tag/';
    public static GetAllGenres: string = 'api/genres';
    public static GetAllTags: string = 'api/tags';

    public static CreateFanfic: string = 'api/fanfic/create';
    public static GetFanfic: string = 'api/fanfic/';
    public static EditFanfic: string = 'api/fanfic/edit';
    public static DeleteFanfic: string = 'api/fanfic/delete/';
    
    public static CreateTag: string = 'api/tag/create';
    public static AddTag: string = 'api/tag/add/';
    public static DeleteTag: string = 'api/tag/delete/';

    public static GetCommentsCount: string = 'api/fanfic/comment-count/';
    public static GetFanficComments: string = 'api/comments/';
    public static CreateComment: string = 'api/comment/create/';
    public static DeleteComment: string = 'api/comment/delete/';

    public static GetCommentRatings: string = 'api/comment/ratings/';
    public static CreateCommentRating: string = 'api/comment/rating/create/';
    public static DeleteCommentRating: string = 'api/comment/rating/delete';

    public static CreateChapter: string = 'api/chapter/create/';
    public static EditChapter: string = 'api/chapter/edit/';
    public static DeleteChapter: string = 'api/chapter/delete/';
    public static GetFanficChapters: string = 'api/chapters/';

    public static GetUserProfile: string = 'api/user/'; // + username as id
    public static GetUserFanfics: string = 'api/user-fanfics/'; // + username as id
    
}