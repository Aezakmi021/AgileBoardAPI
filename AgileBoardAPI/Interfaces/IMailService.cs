using AgileBoardAPI.DTO;

namespace AgileBoardAPI.Interfaces
{
    public interface IMailService
    {
        Task SendConfirmationMail(Mail mail);
    }
}
