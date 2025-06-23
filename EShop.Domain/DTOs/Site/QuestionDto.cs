using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Site;

public class QuestionDto
{
    #region Properties

    public long Id { get; set; }

    [Display(Name = "عنوان سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string QuestionTitle { get; set; }

    [Display(Name = "جواب سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }

    #endregion
}