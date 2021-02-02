using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Zack.Weixin.MiniProgram;

namespace ConsoleTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddUserSecrets(typeof(Program).Assembly);
            IConfigurationRoot configRoot = configBuilder.Build();
            string appId = configRoot["AppId"];
            string appSecret = configRoot["AppSecret"];
            ServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            services.AddScoped<AuthApi>();
            services.AddScoped<WXACodeApi>();

            using (var sp = services.BuildServiceProvider())
            {
                /*
                var authApi = sp.GetRequiredService<AuthApi>();
                var getAccessTokenResult = await authApi.GetAccessTokenAsync(appId, appSecret);
                string accessToken = getAccessTokenResult.Access_token;
                Console.WriteLine(getAccessTokenResult.Access_token);
                var aCodeApi = sp.GetRequiredService<WXACodeApi>();
                var r = await aCodeApi.GetUnlimitedAsync(accessToken, "a", null);
                if (r.Errcode == 0)
                {
                    File.WriteAllBytes("d:/1.jpg", r.Picture);
                }
                else
                {
                    Console.WriteLine(r.Errmsg);
                }*/
                var authApi = sp.GetRequiredService<AuthApi>();
                //code只能用一次
                string code = "091YGwGa1h6BsA0K23Ga1KrZUJ1YGwGI";
                string encryptedData = "tBfph8cMa2z/CvXVrxemSzBKkmeAQSJD0zveHylwPqV58Ir1mQiXdPYYaAPSK4zkwIJbu5GHWJKGRqLA8B7xx1ThwX53l7w82T4DDMK3mAipAGax1oxb8CoHAzU0ZuXgT4ejdNnf+hty1QKWY4X3UBoLt0Ies32AGYk1YelfRVxStVJ2Tf2BGHuHli3k4t1eH3Ra/3y9i8d9+kCF3sypwZY/T0mSR9GMqlWLVR0/NrH0vGHqqBRL48dikcgZA1+4Qy9R+Z/Anz4BCj2xbBQ1euCIRA7SAcB8x9oswxwh3DbY/46sYo7BcooLAv0lPOUQjKFlW4osA0qv4rtKW4qTAVebmgZcYNRAnco9BGWlqFCSPobB9rrX2M5d6AQLcFg2bmuceWqXCXY0LIfAr6NZpYcLggrCcHfUtVt+Lv9R9cWtTwYgjNE3ywjMhNUxN8Gv2JTxEe98V/Rbu590ZuTvOuxUfVU91Ko18+Bc3PcILd8=";
                string iv = "LqETptbtEeGsEmpJgLdufA==";
                var userInfo = await authApi.GetUserInfoAsync(appId,appSecret,code,encryptedData,iv);
            }
        }
    }
}