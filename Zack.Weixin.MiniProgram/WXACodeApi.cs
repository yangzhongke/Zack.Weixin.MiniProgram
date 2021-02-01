using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Zack.Weixin.MiniProgram.Models;

namespace Zack.Weixin.MiniProgram
{
    public class WXACodeApi
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WXACodeApi(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        //https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/qr-code/wxacode.getUnlimited.html
        public async Task<GetUnlimitedResult> GetUnlimitedAsync(string access_token,string scene,string page, int width=430,bool auto_color=false,Color? line_color=null,bool is_hyaline=false)
        {
            string url = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token="+access_token;
            object lineColorObj = null;
            if(line_color!=null)
            {
                lineColorObj = new { line_color.Value.R, line_color.Value.G , line_color.Value.B }; 
            }            
            var data = new { scene ,page,width,auto_color, line_color = lineColorObj , is_hyaline };
            var httpClient = httpClientFactory.CreateClient();
            string dataJson = data.Serialize();
            
            using (var httpContent = new StringContent(dataJson))
            {
                var resp = await httpClient.PostAsync(url, httpContent);
                string contentType = resp.Content.Headers.ContentType.MediaType;
                if(contentType== "application/json")
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    return json.Deserialize<GetUnlimitedResult>();
                }
                else
                {
                    GetUnlimitedResult result = new GetUnlimitedResult();
                    result.Picture = await resp.Content.ReadAsByteArrayAsync();
                    return result;
                }
            }                
        }
    }
}
