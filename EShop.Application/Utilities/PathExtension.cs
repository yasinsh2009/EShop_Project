namespace EShop.Application.Utilities;

public static class PathExtension
{
    #region Default Images

    public static string DefaultAvatar = "/MainTemplate/assets/image/user/user.png";

    #endregion

    #region User Avatar

    public static string UserAvatarOrigin = "/Content/Images/UserAvatar/Origin/";
    public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Origin/");

    public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb";
    public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Thumb/");

    #endregion
}