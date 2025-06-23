using EShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.Site;

public class Question : BaseEntity
{
    #region Properties

    [Display(Name = "عنوان سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string QuestionTitle { get; set; }

    [Display(Name = "جواب سوال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }

    #endregion
}