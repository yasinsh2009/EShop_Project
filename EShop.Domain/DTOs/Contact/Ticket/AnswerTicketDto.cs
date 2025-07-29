using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Contact.Ticket
{
    public class AnswerTicketDto
    {
        public long Id { get; set; }

        [Display(Name = "پاسخ تیکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Text { get; set; }
    }

    public enum AnswerTicketResult
    {
        NotForUser,
        NotFound,
        Success,
        Error
    }
}
