using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities.Account.User;
using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.Contact.Ticket
{
    public class TicketMessage : BaseEntity
    {
        #region Properties

        public long TicketId { get; set; }
        public long SenderId { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }

        #endregion

        #region Relations

        public Ticket Ticket { get; set; }
        public User Sender { get; set; }

        #endregion
    }
}
