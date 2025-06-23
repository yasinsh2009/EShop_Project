using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Contact;
using EShop.Domain.Entities.Contact;
using EShop.Domain.Repository.Interface;

namespace EShop.Application.Services.Implementation;

public class ContactService : IContactService
{
    #region Contructor

    private readonly IGenericRepository<ContactUs> _contactRepository;

    public ContactService(IGenericRepository<ContactUs> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    #endregion

    #region Services

    public async Task SendNewContactMessage(SendContactMessageDto contact, string userIp, long? userId)
    {
        // todo: Use Sanitizer to Sanitize The input data

        var newMessage = new ContactUs
        {
            UserId = (userId != null && userId.Value != 0) ? userId.Value : (long?)userId,
            UserIp = userIp,
            Fullname = contact.Fullname,
            Email = contact.Email,
            MessageSubject = contact.MessageSubject,
            MessageText = contact.MessageText,
        };

        await _contactRepository.AddEntity(newMessage);
        await _contactRepository.SaveChanges();
    }

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_contactRepository != null)
        {
            _contactRepository.DisposeAsync();
        }
    }

    #endregion
}