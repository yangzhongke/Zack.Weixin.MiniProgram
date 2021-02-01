namespace Zack.Weixin.MiniProgram.Models
{
    public class Code2SessionResult:BaseResult
    {
        public string OpenId { get; set; }
        public string Session_Key { get; set; }
        /// <summary>
        /// https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/union-id.html
        /// 只有满足特定条件才会返回UnionId，一般是同主体下有多个微信应用，且用户已经关注了他们。
        /// </summary>
        public string UnionId { get; set; }
    }
}
