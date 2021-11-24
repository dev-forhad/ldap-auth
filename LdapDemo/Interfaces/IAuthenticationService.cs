using LdapDemo.Models;

namespace LdapDemo.Interfaces
{
    public interface IAuthenticationService
    {
        User Login(string userName, string password);
    }
}
