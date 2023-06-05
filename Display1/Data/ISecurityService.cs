using Display1.Models;


namespace Display1.Data
{
    public interface ISecurityService
    {
        bool ValidateCredentials(string password, Password dbPassword);
    }
}