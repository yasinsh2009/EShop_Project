namespace EShop.Application.Services.Interface;

public interface ISmsService : IAsyncDisposable
{
    Task SendVerificationSms(string mobile, string activationCode);
}