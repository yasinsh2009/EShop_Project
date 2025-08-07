using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Site
{
    public class CreateAboutUsDto
    {
        #region properties

        [Display(Name = "متن هدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string HeaderTitle { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }



        #endregion
    }

    public enum CreateAboutUsResult
    {
        Success,
        Error
    }
}
