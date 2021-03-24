using RestSharp;
using Auth0.AuthenticationApi;

namespace Pitcher.Models
{
    public class LoginAuthentication
    {

        public static Auth0Token Login(string ClientID, string ClientSecret, string domain)
        {
            // var client = new RestClient($"https://{domain}/oauth/token");
            // var request = new RestRequest(Method.POST);
            // request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={ClientID}&client_secret={ClientSecret}&audience=https%3A%2F%2Fdev-dgdfgfdgf324.au.auth0.com%2Fapi%2Fv2%2F", ParameterType.RequestBody);
            // IRestResponse response = client.Execute(request);
            var authenticationApiClient = new AuthenticationApiClient(domain);

            // Get the access token
            var token =  authenticationApiClient.GetTokenAsync(new Auth0.AuthenticationApi.Models.ClientCredentialsTokenRequest
            {                
                ClientId = ClientID,
                ClientSecret = ClientSecret,
                Audience = "https://dev-dgdfgfdgf324.au.auth0.com/api/v2/"
            }).Result;
            return new Auth0Token {strAuthToken = token.AccessToken};   
        }
    }
}