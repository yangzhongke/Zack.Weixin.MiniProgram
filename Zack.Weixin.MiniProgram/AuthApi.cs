using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zack.Weixin.MiniProgram.Models;

namespace Zack.Weixin.MiniProgram
{
    public class AuthApi
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AuthApi(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<GetAccessTokenResult> GetAccessTokenAsync(string appid, string secret)
        {
            var httpClient = httpClientFactory.CreateClient();
            string grant_type = "client_credential";
            string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type={grant_type}&appid={appid}&secret="+Uri.EscapeDataString(secret);
            string json = await httpClient.GetStringAsync(url);
            return json.Deserialize< GetAccessTokenResult>();
        }

        //https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/login/auth.code2Session.html
        public async Task<Code2SessionResult> Code2SessionAsync(string appid,string secret,string js_code)
        {
            var httpClient = httpClientFactory.CreateClient();
            string grant_type = "authorization_code";
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appid}&secret={secret}&js_code={js_code}&grant_type=authorization_code" + grant_type;
            string json = await httpClient.GetStringAsync(url);
            return json.Deserialize<Code2SessionResult>();
        }

        public static string DecodeUserInfo(string encryptedData, string iv, string code,
            string session_key)
        {
            byte[] iv2 = Convert.FromBase64String(iv);

            if (string.IsNullOrEmpty(encryptedData))
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }
            byte[] toEncryptArray = Convert.FromBase64String(encryptedData);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Convert.FromBase64String(session_key),
                IV = iv2,
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

           var cTransform = rm.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
