namespace Crud.Application.Features.Training.Services
{
    public interface IEmailService
    {
        void SendAccountCreationEmail(string email);
    }
}