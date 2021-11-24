using LdapDemo.Models;
using System.Collections.Generic;

namespace LdapDemo.Interfaces
{
    public interface IAuthenticationService
    {
        User Login(string userName, string password);
        void GetUsers();
        List<User> GetADUsers();
    }
}
