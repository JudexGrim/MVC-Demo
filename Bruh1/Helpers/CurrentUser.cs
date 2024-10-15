using System.Security.Claims;

namespace MVC.Helpers
{
    public static class CurrentUser
    {
        public static ClaimsPrincipal User { get; private set; }
                
        public static void SetUser (ClaimsPrincipal user)
        {
            User = user;
        }

        public static int GetUserID()
        {
                return Int32.Parse(User.FindFirst("ID")?.Value ?? "0");
        }

        public static string GetUsername()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "null";
        }

        public static string GetUserEmail()
        {
            return User.FindFirst("Email")?.Value ?? "null";
        }

        public static string GetUserJWT()
        {
            return User.FindFirst("JWT Token")?.Value ?? "null";
        }

        public static string GetRole()
        {
            return User.FindFirst("Role")?.Value ?? "User";
        }
    }
}
