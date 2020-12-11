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
        
    }
}