using EShop.Domain.DTOs.Contact;

namespace EShop.Application.Services.Interface;

public interface IContactService : IAsyncDisposable
{
    Task SendNewContactMessage(SendContactMessageDto contact, string userIp, long? userId);
}