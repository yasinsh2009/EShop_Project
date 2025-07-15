using EShop.Domain.Entities.Account.User;
using EShop.Domain.Entities.Contact.Ticket;

namespace EShop.Domain.DTOs.Contact.Ticket
{
    public class TicketDetailDto
    {
        public Entities.Contact.Ticket.Ticket Ticket { get; set; }
        public User Owner { get; set; }
        public List<TicketMessage> TicketMessage { get; set; }
    }
}
