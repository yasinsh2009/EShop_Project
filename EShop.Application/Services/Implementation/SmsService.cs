using EShop.Application.Services.Interface;
using Microsoft.Extensions.Configuration;

namespace EShop.Application.Services.Implementation;

public class SmsService : ISmsService
{
    #region Constructor

    private readonly IConfiguration _configuration;

    public SmsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion

    #region Services

    #region Send Verification Code

    public async Task SendVerificationSms(string mobile, string activationCode)
    {
        var apiKey = _configuration.GetSection("KavenegarSmsApiKey")["apiKey"];
        var api = new Kavenegar.KavenegarApi(apiKey);

        await api.VerifyLookup(mobile, activationCode, "VerifyWebsiteAccount");
    }

    #endregion

    #region Restore User Password

    public async Task SendRestorePasswordSms(string mobile, string newPassword)
    {
        var apiKey = _configuration.GetSection("KavenegarSmsApiKey")["apiKey"];
        var api = new Kavenegar.KavenegarApi(apiKey);

        await api.VerifyLookup(mobile, newPassword, "VerifyRecoverPassword");
    }

    #endregion

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync() { }

    #endregion
}