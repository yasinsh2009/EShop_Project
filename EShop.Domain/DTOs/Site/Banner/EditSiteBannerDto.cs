namespace EShop.Domain.DTOs.Site.Banner
{
    public class EditSiteBannerDto : CreateSiteBannerDto
    {
        #region Properties
        public long Id { get; set; }

        #endregion

    }

    public enum EditSiteBannerResult
    {
        Success,
        NotFound,
        Error
    }
}
