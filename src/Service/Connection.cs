

namespace AACN.Services
{
    using AACN.API.Service;
    using Azure.Core;
    using Azure.Security.KeyVault.Secrets;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Microsoft.Identity.Client;
    using System.Collections.Concurrent;

    public class Connection
    {

        public static readonly string CrmApiUrl = ConnectionConfig.configData.GetSection("AppConnection:CRM:CrmApiUrl").Value;
        public static readonly string CrmResourceUrl = ConnectionConfig.configData.GetSection("AppConnection:CRM:CrmResourceUrl").Value;
        public static readonly string CrmclientId = ConnectionConfig.configData.GetSection("AppConnection:CRM:CrmclientId").Value;
        public static readonly string CrmClientSecret = ConnectionConfig.configData.GetSection("AppConnection:CRM:CrmClientSecret").Value;
        public static readonly string authority = ConnectionConfig.configData.GetSection("AppConnection:CRM:authority").Value;


        public static string CrmBearerToken { get; set; } = null;


        #region oldCode
        //public static async Task<string> AccessTokenGenerator()
        //{
        //    try
        //    {
        //        var app = ConfidentialClientApplicationBuilder.Create(CrmclientId)
        //            .WithClientSecret(CrmClientSecret)
        //            .WithAuthority(new Uri(authority))
        //            .Build();
        //        var scopes = new[] { $"{CrmResourceUrl}/.default" };
        //        var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
        //        return result.AccessToken;
        //    }
        //    catch (MsalServiceException ex)
        //    {
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception occurred: {ex.Message}");
        //        return null;
        //    }
        //}

        #endregion
        public static async Task<string> AccessTokenGenerator()
        {
            var key = $"CRMService_{CrmclientId}"; //Use a meaningful key for your cache entry
            return await TokenCache.GetAccessToken(key, async () =>
                        {
                            try
                            {
                                var app = ConfidentialClientApplicationBuilder.Create(CrmclientId)
                                    .WithClientSecret(CrmClientSecret)
                                    .WithAuthority(new Uri(authority))
                                    .Build();

                                var scopes = new[] { $"{CrmResourceUrl}/.default" };
                                var result = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                                return result.AccessToken;
                            }
                            catch (MsalServiceException ex)
                            {
                                Console.WriteLine($"MsalServiceException occurred: {ex.Message}");
                                throw;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Exception occurred: {ex.Message}");
                                throw;
                            }
                        });
        }

        public static class TokenCache
        {
            private static readonly ConcurrentDictionary<string, (string AccessToken, DateTimeOffset ExpiryTime)> _cache = new ConcurrentDictionary<string, (string, DateTimeOffset)>();

            public static async Task<string> GetAccessToken(string key, Func<Task<string>> acquireToken)
            {
                if (_cache.TryGetValue(key, out var tokenInfo) && tokenInfo.ExpiryTime > DateTimeOffset.UtcNow.AddMinutes(1))
                {
                    return tokenInfo.AccessToken;
                }
                try
                {
                    var accessToken = await acquireToken();
                    var expiryTime = DateTimeOffset.UtcNow.AddMinutes(19); // Assuming token expiry time is 20 minutes (19 minutes is used as a buffer)

                    _cache[key] = (accessToken, expiryTime);

                    return accessToken;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to acquire token: {ex.Message}");
                    return null;
                }
            }
        }


    }

}

