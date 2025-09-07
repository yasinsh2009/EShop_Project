using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.Site
{
    public class SiteBanner : BaseEntity
    {
        #region Properties

        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ImageName { get; set; }

        [Display(Name = "آدرس بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Link { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public SiteBannerGridColumnSize GridColumnSize { get; set; }

        public SiteBannerPlacement Placement { get; set; }

        #endregion
    }

    public enum SiteBannerPlacement
    {
        [Display(Name = "جایگاه اول")]
        FirstPlace,

        [Display(Name = "جایگاه دوم")]
        SecondPlace,

        [Display(Name = "جایگاه سوم")]
        ThirdPlace,

        [Display(Name = "جایگاه چهارم")]
        FourthPlace
    }

    public enum SiteBannerGridColumnSize
    {
        [Display(Name = "اندازه بنر : ۱۲ از ۱۲ (یک)")]
        Col12 = 12,

        [Display(Name = "اندازه بنر : ۱۱ از ۱۲")]
        Col11 = 11,

        [Display(Name = "اندازه بنر : ۱۰ از ۱۲")]
        Col10 = 10,

        [Display(Name = "اندازه بنر : ۹ از ۱۲")]
        Col9 = 9,

        [Display(Name = "اندازه بنر : ۸ از ۱۲")]
        Col8 = 8,

        [Display(Name = "اندازه بنر : ۷ از ۱۲")]
        Col7 = 7,

        [Display(Name = "اندازه بنر : ۶ از ۱۲ (نصف)")]
        Col6 = 6,

        [Display(Name = "اندازه بنر : ۵ از ۱۲")]
        Col5 = 5,

        [Display(Name = "اندازه بنر : ۴ از ۱۲ (ربع)")]
        Col4 = 4,

        [Display(Name = "اندازه بنر : ۳ از ۱۲ (ثلث)")]
        Col3 = 3,

        [Display(Name = "اندازه بنر : ۲ از ۱۲")]
        Col2 = 2,

        [Display(Name = "اندازه بنر : ۱ از ۱۲")]
        Col1 = 1,
    }

}
