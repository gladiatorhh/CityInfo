namespace CityInfo.Services
{
    public interface IMailService
    {
        void SendMail(string subject, string body);
    }
}