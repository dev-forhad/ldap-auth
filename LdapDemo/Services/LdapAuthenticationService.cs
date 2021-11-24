using LdapDemo.Interfaces;
using LdapDemo.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace LdapDemo.Services
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LdapConfig config;

        public LdapAuthenticationService(IOptions<LdapConfig> config)
        {
            this.config = config.Value;
        }

        public User Login(string userName, string password)
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                        searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                        searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                        var result2 = searcher.FindAll();
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            var displayName = result.Properties[DisplayNameAttribute];
                            var samAccountName = result.Properties[SAMAccountNameAttribute];

                            return new User
                            {
                                DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                                UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
            }
            return null;
        }

        public void GetUsers()
        {
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        var a = searcher.FindAll();
                        string b = "forhad";
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
            }
        }

        public List<User> GetADUsers()
        {
            List<User> lstADUsers = new List<User>();
            try
            {
                DirectoryEntry searchRoot = new DirectoryEntry(config.Path,  config.UserDomainName + "\\" + config.UserName, config.Password);
                DirectorySearcher search = new DirectorySearcher(searchRoot);
                search.Filter = "(&(objectClass=user)(objectCategory=person))";
                search.PropertiesToLoad.Add(DisplayNameAttribute);
                search.PropertiesToLoad.Add(SAMAccountNameAttribute);
                
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();
                if (resultCol != null)
                {
                    
                    for (int counter = 0; counter < resultCol.Count; counter++)
                    {
                        string UserNameEmailString = string.Empty;
                        result = resultCol[counter];

                        var displayName = result.Properties[DisplayNameAttribute];
                        var samAccountName = result.Properties[SAMAccountNameAttribute];

                        User user = new User
                        {
                            DisplayName = displayName == null || displayName.Count <= 0 ? null : displayName[0].ToString(),
                            UserName = samAccountName == null || samAccountName.Count <= 0 ? null : samAccountName[0].ToString()
                        };
                        lstADUsers.Add(user);

                    }
                }
                return lstADUsers;
            }
            catch (Exception ex)
            {
                return lstADUsers;
            }
        }
    }
}
