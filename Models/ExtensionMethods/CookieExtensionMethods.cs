using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace Group7_FinalProject.Models
{
    public static class CookieExtensions
    {
        public static string GetString(this IRequestCookieCollection cookies, string key) =>
             cookies[key];

        public static int? GetInt32(this IRequestCookieCollection cookies, string key) =>
            int.TryParse(cookies[key], out int i) ? i : (int?)null;

        public static T GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            var value = cookies[key];
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }


        public static void SetString(this IResponseCookies cookies, string key,
            string value, int days = 30)
        {
            cookies.Delete(key);
            if (days == 0)
            {
                cookies.Append(key, value);
            }
            else
            {
                CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(days) };
                cookies.Append(key, value, options);
            }

        }
        public static void SetInt32(this IResponseCookies cookies, string key,
            int value, int days = 30) =>
            cookies.SetString(key, value.ToString(), days);

        public static void SetObject<T>(this IResponseCookies cookies, string key,
            T value, int days = 30) =>
            cookies.SetString(key, JsonConvert.SerializeObject(value), days);
    }
}
