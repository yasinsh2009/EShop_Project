using EShop.Domain.DTOs.Contact;
using EShop.Domain.DTOs.Contact.Ticket;

namespace EShop.Application.Services.Interface;

public interface IContactService : IAsyncDisposable
{
    #region Contact Us

    Task SendNewContactMessage(SendContactMessageDto contact, string userIp, long? userId);

    #endregion

    #region Ticket

    Task<AddTicketResult> AddUserTicket(AddTicketDto ticket, long userId);
    Task<FilterTicketDto> TicketsList(FilterTicketDto ticket);
    Task<TicketDetailDto> GetTicketDetail(long ticketId, long userId);

    #endregion
}