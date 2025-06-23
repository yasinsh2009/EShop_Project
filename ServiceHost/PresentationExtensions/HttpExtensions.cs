﻿namespace ServiceHost.PerstationExtentions;

public static class HttpExtensions
{
    public static string GetUserIp(this HttpContext httpContext)
    {
        return httpContext.Connection.RemoteIpAddress.ToString();
    }
}