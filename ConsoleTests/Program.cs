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
                string code = "091zDDkl2wMjr64ycBnl2PLORd3zDDk6";
                var sessionResult = await authApi.Code2SessionAsync(appId, appSecret, code);
            }
        }
    }
}