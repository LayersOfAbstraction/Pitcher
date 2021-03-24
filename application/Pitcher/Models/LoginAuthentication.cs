using RestSharp;

namespace Pitcher.Models
{
    public class LoginAuthentication
    {

        public static Auth0Token Login(string ClientID, string ClientSecret, string domain)
        {
            var client = new RestClient($"https://{domain}/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={ClientID}&client_secret={ClientSecret}&audience=https%3A%2F%2F%24%7Baccount.namespace%7D%2Fapi%2Fv2%2F", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return new Auth0Token {strAuthToken = ConstantStrings.strToken};   
        }
    }
}