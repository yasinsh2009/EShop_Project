namespace EShop.Application.Utilities;

public static class PathExtension
{
    #region Default Images

    public static string DefaultAvatar = "/MainTemplate/assets/image/user/user.png";
    public static string NoImage = "/AdminTemplate/assets/images/NoImage.jpg";

    #endregion

    #region User Avatar

    public static string UserAvatarOrigin = "/Content/Images/UserAvatar/Origin/";
    public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Origin/");

    public static string UserAvatarThumb = "/Content/Images/UserAvatar/Thumb";
    public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/UserAvatar/Thumb/");

    #endregion

    #region Uploader

    public static string UploaderImage = "/MainTemplate/assets/ckEditorImageUpload/";
    public static string UploaderImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MainTemplate/assets/ckEditorImageUpload/");

    #endregion

    #region Slider

    public static string SliderOriginTemp = "/Theme/assets/image/";
    public static string MobileSliderOriginTemp = "/Theme/assets/image/mobile/";

    public static string SliderOrigin = "/Content/Images/Slider/Origin/";
    public static string SliderOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Slider/Origin/");

    public static string MobileSliderOrigin = "/Content/Images/MobileSlider/Origin/";
    public static string MobileSliderOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/MobileSlider/Origin/");

    public static string SliderThumb = "/Content/Images/Slider/Thumb/";
    public static string SliderThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Slider/Thumb/");

    public static string MobileSliderThumb = "/Content/Images/MobileSlider/Thumb/";
    public static string MobileSliderThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/MobileSlider/Thumb/");

    #endregion

    #region Site Banner

    public static string SiteBannerOriginTemp = "/Theme/assets/image/";

    public static string SiteBannerOrigin = "/Content/Images/SiteBanner/Origin/";
    public static string SiteBannerOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/SiteBanner/Origin/");

    public static string SiteBannerThumb = "/Content/Images/SiteBanner/Thumb/";
    public static string SiteBannerThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/SiteBanner/Thumb/");

    #endregion

    #region Products

    public static string ProductOrigin = "/Content/Images/Product/Origin/";
    public static string ProductOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Product/Origin/");

    public static string ProductThumb = "/Content/Images/Product/Thumb/";
    public static string ProductThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Product/Thumb/");

    #endregion
}