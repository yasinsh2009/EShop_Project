using ECommerceApp.Domain.Entities.Account.User;
using ECommerceApp.Domain.Entities.Contact.Ticket;

namespace ECommerceApp.Domain.DTOs.Contact.Ticket
{
    public class TicketDetailDto
    {
        public Entities.Contact.Ticket.Ticket Ticket { get; set; }
        public User Owner { get; set; }
        public List<TicketMessage> TicketMessage { get; set; }
    }
}
