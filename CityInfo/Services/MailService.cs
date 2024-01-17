namespace CityInfo.Services;

public class MailService : IMailService
{
    private readonly string _fromMail;
    private readonly string _toMail;

    public MailService(IConfiguration configuration)
    {
        _fromMail = configuration["MailSettings:SiteMail"];
        _toMail = configuration["MailSettings:AdminMail"];
    }

    public void SendMail(string subject, string body)
    {
        Console.WriteLine($"mail sent to {_toMail} from {_fromMail}");
        Console.WriteLine($"subject {subject}");
        Console.WriteLine($"body : {body}");
    }
}
