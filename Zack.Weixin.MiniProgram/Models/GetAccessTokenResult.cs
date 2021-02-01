using System;
using System.Collections.Generic;
using System.Text;

namespace Zack.Weixin.MiniProgram.Models
{
    public class GetAccessTokenResult: BaseResult
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
    }
}
