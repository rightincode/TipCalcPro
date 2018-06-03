using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tipcalc_auth.Interfaces;
using Xamarin.Forms;

namespace tipcalc_auth.Models
{
    public class AuthenticationProvider : IAuthenticate
    {
        //TODO: relocate and secure these string statics
        public static readonly string Tenant = "tipcalcprodemo.onmicrosoft.com";
        public static readonly string ClientID = "41179e6b-e135-4de7-9f6e-5bbad25c8ba0";
        public static readonly string SignUpAndInPolicy = "B2C_1_GenericSignUpAndIn";

        public static readonly string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        public static readonly string Authority = $"{AuthorityBase}{SignUpAndInPolicy}";

        public static readonly string[] Scopes = new string[] { "https://tipcalcprodemo.onmicrosoft.com/backends/user_impersonation" };

        public static readonly string URLScheme = "msal41179e6b-e135-4de7-9f6e-5bbad25c8ba0";
        public static readonly string RedirectUri = $"{URLScheme}://auth";

        public static PublicClientApplication AuthClient { get; private set; }

        public AuthenticationProvider()
        {
            AuthClient = new PublicClientApplication(ClientID, Authority);
            AuthClient.RedirectUri = RedirectUri;
        }

        public async Task<bool> LoginAsync(bool useSilent = false)
        {
            bool success = false;
            try
            {
                AuthenticationResult authenticationResult;

                if (useSilent)
                {
                    authenticationResult = await AuthClient.AcquireTokenSilentAsync(
                        Scopes,
                        GetUserByPolicy(AuthClient.Users, SignUpAndInPolicy),
                        Authority,
                        false);
                }
                else
                {
                    authenticationResult = await AuthClient.AcquireTokenAsync(
                        Scopes,
                        GetUserByPolicy(AuthClient.Users, SignUpAndInPolicy),
                        App.UiParent);
                }

                //if (User == null)
                //{
                //    var payload = new JObject();
                //    if (authenticationResult != null && !string.IsNullOrWhiteSpace(authenticationResult.IdToken))
                //    {
                //        payload["access_token"] = authenticationResult.IdToken;
                //    }

                    //User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(
                    //    MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory,
                    //    payload);
                    //success = true;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userId = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userId.EndsWith(policy.ToLower(), StringComparison.Ordinal))
                    return user;
            }
            return null;
        }

        string Base64UrlDecode(string str)
        {
            str = str.Replace('-', '+').Replace('_', '/');
            str = str.PadRight(str.Length + (4 - str.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(str);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
