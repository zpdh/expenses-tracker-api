﻿namespace ExpensesTracker.Infrastructure.Authentication.Jwt;

public sealed class JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
}