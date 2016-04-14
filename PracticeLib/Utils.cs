using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PracticeLib
{
    public static class Utils
    {
        public static string ToJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static T ParseJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static byte[] ToBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string ParseBytes(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ToJsonBytes<T>(T value)
        {
            return ToBytes(ToJson(value));
        }
        public static T ParseJsonBytes<T>(byte[] bytes)
        {
            return ParseJson<T>(ParseBytes(bytes));
        }
    }
}
