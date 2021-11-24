using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdapDemo.Models
{
    public class LdapConfig
    {
        public string Path { get; set; }
        public string UserDomainName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
