namespace EShop.Application.Services.Interface;

public interface ISmsService : IAsyncDisposable
{
    #region Activation User Account

    Task SendVerificationSms(string mobile, string activationCode);

    #endregion

    #region Restore User Password

    Task SendRestorePasswordSms(string mobile, string newPassword);

    #endregion

}