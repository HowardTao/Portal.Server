using System;
using System.Text;
using PortalAdmin.Common.Extensions;

namespace PortalAdmin.Common.Utils
{
    public static class ConvertHelper
    {
        /// <summary>
        /// 转换为 16 进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">是否大小写</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes, bool lowerCase = true)
        {
            if (bytes == null) return null;
            var result = new StringBuilder();
            var format = lowerCase ? "x2" : "X2";
            foreach (var b in bytes)
            {
                result.Append(b.ToString(format));
            }
            return result.ToString();
        }

        /// <summary>
        /// 字符转 16进制转字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string s)
        {
            if (s.IsNull()) return null;
            var bytes = new byte[s.Length/2];
            for (var x = 0; x < s.Length; x++)
            {
                var i = Convert.ToInt32(s.Substring(x * 2, 2), 16);
                bytes[x] = (byte) i;
            }
            return bytes;
        }

        public static string ToBase64(this byte[] bytes)
        {
            return bytes != null ? Convert.ToBase64String(bytes) : null;
        }
    }
}