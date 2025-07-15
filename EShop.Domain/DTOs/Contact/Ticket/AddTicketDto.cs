using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities.Contact.Ticket;

namespace EShop.Domain.DTOs.Contact.Ticket
{
    public class AddTicketDto
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "بخش مورد نظر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public TicketSection TicketSection { get; set; }

        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public TicketPriority TicketPriority { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
    }

    public enum AddTicketResult
    {
        Error,
        EmptyText,
        Success
    }
}
