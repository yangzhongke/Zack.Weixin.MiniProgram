using System.Text.Json;

namespace Zack.Weixin.MiniProgram
{
    public static class JsonHelper
    {
        public static string Serialize(this object obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        public static T Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }
}
