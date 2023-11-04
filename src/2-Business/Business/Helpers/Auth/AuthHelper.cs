using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Business.Helpers.Auth;

public class AuthHelper
{
    public static string GetName(IHttpContextAccessor accessor)
    {
        return accessor.HttpContext!.User.Identity!.Name!;
    }

    public static bool IsAuthenticated(IHttpContextAccessor accessor)
    {
        return accessor.HttpContext!.User.Identity!.IsAuthenticated;
    }

    public static string GetUserEmail(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return "";

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        var email = claims.FindFirst(ClaimTypes.Name);

        return email!.Value;
    }

    public static Guid GetUserId(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return Guid.Empty;

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        var GuidId = claims.FindFirst(ClaimTypes.NameIdentifier);

        return Guid.Parse(GuidId!.Value);
    }
    public static ClaimsPrincipal GetLoggedInUser(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return null!;

        return accessor.HttpContext!.User;
    }

    public static bool IsAdmin(IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
    {
        var loggedInUser = GetLoggedInUser(accessor);

        if (loggedInUser == null)
        {
            return false;
        }

        var loggedInUserId = GetUserId(accessor);
        var userRoles = userManager.GetRolesAsync(userManager.FindByIdAsync(loggedInUserId.ToString()).Result).Result;

        return userRoles.Contains("Admin");
    }

    public static List<Claim> GetUserClaims(IHttpContextAccessor accessor)
    {
        if (!IsAuthenticated(accessor)) return null!;

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        if (claims == null)
        {
            return new List<Claim>();
        }
        return claims.Claims.ToList();
    }
    public static bool UserHasClaim(IHttpContextAccessor accessor, string claimType, string claimValue)
    {
        if (!IsAuthenticated(accessor)) return false;

        ClaimsPrincipal claims = accessor.HttpContext!.User;
        if (claims == null)
        {
            return false;
        }
        var claimList = claims.Claims.ToList();
        return claimList.Any(claim =>
            claim.Type == claimType && claim.Value.Contains(claimValue));
    }
}