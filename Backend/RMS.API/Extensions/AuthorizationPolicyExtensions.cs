using Microsoft.AspNetCore.Authorization;
using RMS.Application.Constants;

namespace RMS.API.Extensions;

public static class AuthorizationPolicyExtensions
{
    public static void AddPermissionPolicy(
        this AuthorizationOptions options,
        string permission)
    {
        options.AddPolicy(permission, policy =>
            policy.RequireClaim("permission", permission));
    }

    public static void AddRmsPermissionPolicies(
        this AuthorizationOptions options)
    {
        options.AddPermissionPolicy(Permission.Users.View);
        options.AddPermissionPolicy(Permission.Users.Upsert);
        options.AddPermissionPolicy(Permission.Users.Delete);
    }
}