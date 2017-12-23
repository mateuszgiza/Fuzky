using Fuzky.Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fuzky.Core
{
    public class SteamAuthentication
    {
        public const string LoginUrl = "https://steamcommunity.com";

        public EventSource<EventArgs> TwoFactorCodeRequired = new EventSource<EventArgs>();
        public EventSource<EventArgs> ExceptionThrown = new EventSource<EventArgs>();

        public async Task<LoginResponse> Authenticate(string username, string password, string twoFactorCode)
        {
            LoginResponse response;

            try
            {
                var rsaKey = await GetRsaKey(username);

                response = await Login(username, password, rsaKey, twoFactorCode);
                if (response.RequiresTwofactor)
                {
                    TwoFactorCodeRequired.Raise(this, EventArgs.Empty);
                }
            }
            catch (Exception e)
            {
                this.ExceptionThrown.Raise(this, EventArgs.Empty);
                response = new LoginResponse();
            }

            return response;
        }

        private async Task<RsaKeyResponse> GetRsaKey(string username)
        {
            var form = new Dictionary<string, string>
            {
                ["username"] = username
            };

            return await PerformRequest<RsaKeyResponse>("/login/getrsakey", form);
        }

        private async Task<LoginResponse> Login(string username, string password, RsaKeyResponse rsaKey, string twoFactorCode)
        {
            string encodedCipherPassword;

            // RSA Encryption
            using (var rsa = new RSACryptoServiceProvider())
            {
                var parameters = new RSAParameters
                {
                    Exponent = rsaKey.PublickeyExp.HexToBytes(),
                    Modulus = rsaKey.PublickeyMod.HexToBytes()
                };
                rsa.ImportParameters(parameters);

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var cipherPassword = rsa.Encrypt(passwordBytes, false);
                encodedCipherPassword = Convert.ToBase64String(cipherPassword);
            }

            var form = new Dictionary<string, string>
            {
                ["username"] = username,
                ["password"] = encodedCipherPassword,
                ["twofactorcode"] = twoFactorCode,
                ["emailauth"] = "",
                ["loginfriendlyname"] = "",
                ["captchagid"] = "-1",
                ["captcha_text"] = "",
                ["emailsteamid"] = "",
                ["rsatimestamp"] = rsaKey.Timestamp,
                ["remember_login"] = "false"
            };

            return await PerformRequest<LoginResponse>("/login/dologin", form);
        }

        private async Task<T> PerformRequest<T>(string relativeUrl, IDictionary<string, string> parameters)
        {
            var cookieContainer = new CookieContainer();
            var httpHandler = new HttpClientHandler { CookieContainer = cookieContainer };

            using (var client = new HttpClient(httpHandler))
            {
                client.BaseAddress = new Uri(LoginUrl);

                var content = new FormUrlEncodedContent(parameters);
                var response = client.PostAsync(relativeUrl, content);

                var result = response.Result.Content.ReadAsStringAsync();
                return Deserialize<T>(await result);
            }
        }

        private T Deserialize<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }
    }

    public class RsaKeyResponse
    {
        [JsonProperty("publickey_exp")]
        public string PublickeyExp { get; set; }

        [JsonProperty("publickey_mod")]
        public string PublickeyMod { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("token_gid")]
        public string TokenGid { get; set; }
    }

    public class LoginResponse
    {
        [JsonProperty("login_complete")]
        public bool LoginComplete { get; set; }

        [JsonProperty("requires_twofactor")]
        public bool RequiresTwofactor { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("transfer_parameters")]
        public TransferParameters TransferParameters { get; set; }

        [JsonProperty("transfer_urls")]
        public string[] TransferUrls { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("captcha_needed")]
        public bool CaptchaNeeded { get; set; }

        [JsonProperty("captcha_gid")]
        public int CaptchaGid { get; set; }
    }

    public class TransferParameters
    {
        [JsonProperty("auth")]
        public string Auth { get; set; }

        [JsonProperty("remember_login")]
        public bool RememberLogin { get; set; }

        [JsonProperty("steamid")]
        public string Steamid { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("token_secure")]
        public string TokenSecure { get; set; }

        [JsonProperty("webcookie")]
        public string Webcookie { get; set; }
    }

    public static class StringUtils
    {
        public static byte[] HexToBytes(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}