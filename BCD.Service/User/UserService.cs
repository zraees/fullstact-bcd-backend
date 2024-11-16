// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCD.Domain.Interfaces;
using BCD.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BCD.Service.User;
public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SymmetricSecurityKey _Key;

    public UserService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
    }

    public async Task<Domain.Entities.User> IsAuthenticated(string email, string pwd)
    {
        var user = await _unitOfWork.Users.IsAuthenticated(email, pwd).ConfigureAwait(false);

        if (user != null)
        {
            user.Token = await GetToken(user).ConfigureAwait(false);
        }

        return user;
    }

    public async Task<IEnumerable<Domain.Entities.User>> GetUsersAsync()
    {
        return await _unitOfWork.Users.GetAllAsync().ConfigureAwait(false);
    }

    public async Task<Domain.Entities.User> GetUserByIdAsync(int userId)
    {
        return await _unitOfWork.Users.GetByIdAsync(userId).ConfigureAwait(false);
    }

    private Task<string> GetToken(Domain.Entities.User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim (ClaimTypes.GivenName, user.Username),
        };

        var cred = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);
        var TokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Audience = _configuration["Token:Audience"],
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = cred,
            Issuer = _configuration["Token:Issuer"],
        };

        var tokenhandler = new JwtSecurityTokenHandler();
        var token = tokenhandler.CreateToken(TokenDesc);
        return Task.FromResult(tokenhandler.WriteToken(token));
    }

    //public async Task<IEnumerable<User>> GetUsersAsync()
    //{
    //    return await _unitOfWork.Users.GetUsersAsync();
    //}

    //public async Task<int> CreateUserAsync(User User)
    //{
    //    await _unitOfWork.Users.AddAsync(User);
    //    return await _unitOfWork.SaveAsync();
    //}
}
