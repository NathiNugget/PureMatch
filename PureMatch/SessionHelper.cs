using System.Text.Json;

namespace PureMatch
{
    public static class SessionHelper
    {
        public static void Set<T>(T t, HttpContext context)
        {
            string sessionName = nameof(t);
            string s = JsonSerializer.Serialize(t);
            context.Session.SetString(sessionName, s);
        }

        public static T Get<T>(T t, HttpContext context)
        {
            string sessionName = nameof(t);
            string? s = context.Session.GetString(sessionName);
            if (s == null)
            {
                throw new ArgumentException();
            }
            return JsonSerializer.Deserialize<T>(s)!;
        }

        public static void Clear<T>(T t, HttpContext context)
        {
            context.Session.Remove(nameof(  t));
        }
    }
}
