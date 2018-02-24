export class HttpQueryStrings {
    public static LoginUser: string = '/api/auth/login';
    public static RegiserUser: string = 'api/auth/register';
    public static LogoutUser: string = 'api/auth/logout';
    public static CurrentUser: string = 'api/auth/currentuser';

    public static GetAllFanfics: string = 'api/fanfics';
    public static CreateFanfic: string = 'api/fanfic/create';

    public static GetUserFanfics: string = 'api/user-fanfics/'; // + username as id
    
}