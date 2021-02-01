namespace Zack.Weixin.MiniProgram.Models
{
    public abstract class BaseResult
    {
        public int Errcode { get; set; }
        public string Errmsg { get; set; }
    }
}
