using System.IO;
using Auth0.AuthenticationApi;
using Microsoft.Extensions.Configuration;

namespace Pitcher.Models
{
    public class LoginAuthentication
    {
        //Allow value keys pair access to this class.
        public static IConfiguration AppSetting { get; }

        //Setup static constructor to get uri from appsettings
        static LoginAuthentication()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        public static Auth0Token Login(string ClientID, string ClientSecret, string domain)
        {
            var authenticationApiClient = new AuthenticationApiClient(domain);
            var token =  authenticationApiClient.GetTokenAsync(new Auth0.AuthenticationApi.Models.ClientCredentialsTokenRequest
            {                
                ClientId = ClientID,
                ClientSecret = ClientSecret,
                Audience = LoginAuthentication.AppSetting["AccessTokenManagement:Audience"],
            }).Result;
            return new Auth0Token {strAuthToken = token.AccessToken};   
        }
    }
}