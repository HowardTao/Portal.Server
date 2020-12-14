using System;
using System.Text;
using PortalAdmin.Common.Utils;

namespace PortalAdmin.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串转实体
        /// </summary>
        /// <param name="s"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string s)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// 判断字符串是否为 null或空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNull(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 判断字符串是否不为Null、空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 与字符串进行比较，忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string s, string value)
        {
            return s.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 转为Base64
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToBase64(this string s, Encoding encoding)
        {
            if (s.IsNull()) return String.Empty;
            var bytes = encoding.GetBytes(s);
            return bytes.ToBase64();
        }

        /// <summary>
        /// 转换为路径
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToPath(this string s)
        {
            return s.IsNull() ? string.Empty : s.Replace(@"\", "/");
        }
    }
}