using EcommerApp.Domain.Entities.Account.User;
using EcommerApp.Domain.Entities.Contact.Ticket;

namespace EcommerApp.Domain.DTOs.Contact.Ticket
{
    public class TicketDetailDto
    {
        public Entities.Contact.Ticket.Ticket Ticket { get; set; }
        public User Owner { get; set; }
        public List<TicketMessage> TicketMessage { get; set; }
    }
}
