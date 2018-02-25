export class HttpQueryStrings {
    public static LoginUser: string = '/api/auth/login';
    public static RegiserUser: string = 'api/auth/register';
    public static LogoutUser: string = 'api/auth/logout';
    public static CurrentUser: string = 'api/auth/currentuser';

    public static GetAllFanfics: string = 'api/fanfics';
    public static GetAllGenres: string = 'api/genres';
    public static GetAllTags: string = 'api/tags';

    public static CreateFanfic: string = 'api/fanfic/create';
    public static CreateChapter: string = 'api/fanfic/createchapter/';
    public static CreateTag: string = 'api/tag/create';

    public static GetFanfic: string = 'api/fanfic/';
    public static EditFanfic: string = 'api/fanfic/edit';
    public static DeleteFanfic: string = 'api/fanfic/delete/';

    public static GetUserProfile: string = 'api/user/'; // + username as id
    public static GetUserFanfics: string = 'api/user-fanfics/'; // + username as id
    
}