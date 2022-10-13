namespace Pharmacy.WebApi.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor = null!;
        public static HttpResponse Response => Accessor.HttpContext.Response;
        public static HttpRequest Request => Accessor.HttpContext.Request;
        public static IHeaderDictionary ResponseHeaders => Response.Headers;
        public static long UserId => int.Parse(Accessor.HttpContext.User.FindFirst("Id")?.Value ?? "0");
        public static HttpContext HttpContext => Accessor?.HttpContext;
        public static string UserRole => HttpContext?.User.FindFirst("UserRole")?.Value;
        private static long? GetUserId()
        {
            long id;
            bool canParse = long.TryParse(HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value, out id);
            return canParse ? id : null;
        }


    }
}
