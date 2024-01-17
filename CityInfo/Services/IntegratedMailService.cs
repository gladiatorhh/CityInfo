namespace CityInfo.Services;

public class IntegratedMailService : IMailService
{
    private readonly string _fromMail;
    private readonly string _toMail;

    public IntegratedMailService(IConfiguration configuration)
    {
        _fromMail = configuration["MailSettings:SiteMail"];
        _toMail = configuration["MailSettings:AdminMail"];
    }

    public void SendMail(string subject, string body)
    {
        Console.WriteLine($"Mail from {_fromMail} to {_toMail}");
        Console.WriteLine($"subject {subject} \n body {body} sent with integrated mail service");
    }
}
