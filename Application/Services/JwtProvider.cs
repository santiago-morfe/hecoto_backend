namespace Hecoto.Backend.Application.Services;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hecoto.Backend.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;


public class JwtProvider(JwtSettings settings) : IJwtProvider
{
    private readonly JwtSettings _settings = settings;

    public string GenerateToken(int userId, string email, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim("userId", userId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_settings.AccessTokenExpiryMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (int userId, string email, string role) DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == "userId").Value);
        var email = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        var role = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

        return (userId, email, role);
    }

    public bool ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
            ValidateIssuer = true,
            ValidIssuer = _settings.Issuer,
            ValidateAudience = true,
            ValidAudience = _settings.Audience,
            ValidateLifetime = true,
        };

        try
        {
            handler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}