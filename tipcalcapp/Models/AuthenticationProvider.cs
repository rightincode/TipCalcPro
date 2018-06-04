using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tipcalc.Interfaces;

namespace tipcalc.Models
{
    public class AuthenticationProvider : IAuthenticate
    {
        //TODO: relocate and secure these string statics
        public static readonly string Tenant = "tipcalcprodemo.onmicrosoft.com";
        public static readonly string ClientID = "41179e6b-e135-4de7-9f6e-5bbad25c8ba0";
        public static readonly string SignUpAndInPolicy = "B2C_1_Sign-up_and_Sign-In";

        public static readonly string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        public static readonly string Authority = $"{AuthorityBase}{SignUpAndInPolicy}";

        public static readonly string[] Scopes = new string[] { "https://tipcalcprodemo.onmicrosoft.com/backends/user_impersonation" };

        public static readonly string URLScheme = "msal41179e6b-e135-4de7-9f6e-5bbad25c8ba0";
        public static readonly string RedirectUri = $"{URLScheme}://auth";

        public PublicClientApplication AuthClient { get; set; }

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

                if ((authenticationResult != null) && (!string.IsNullOrEmpty(authenticationResult.IdToken)))
                {
                    success = true;
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
                Debug.WriteLine(ex.Message);
            }
            return success;
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            bool success = false;
            try
            {
                //if (User != null)
                //{
                //    await TodoItemManager.DefaultManager.CurrentClient.LogoutAsync();

                //    foreach (var user in ADB2CClient.Users)
                //    {
                //        ADB2CClient.Remove(user);
                //    }
                //    User = null;
                //    success = true;
                //}

                foreach (var user in AuthClient.Users)
                {
                    AuthClient.Remove(user);
                }
                
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
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
