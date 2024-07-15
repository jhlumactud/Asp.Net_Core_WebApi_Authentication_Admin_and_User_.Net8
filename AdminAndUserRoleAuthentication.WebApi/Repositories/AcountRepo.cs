using AdminAndUserRoleAuthentication.WebApi.Data;
using AdminAndUserRoleAuthentication.WebApi.DTOs;
using AdminAndUserRoleAuthentication.WebApi.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static AdminAndUserRoleAuthentication.WebApi.DTOs.ServiceResponses;

namespace AdminAndUserRoleAuthentication.WebApi.Repositories;

public class AcountRepo(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
{
    public async Task<GeneralResponse> CreateAcount(UserDTO userDTO)
    {
        if (userDTO is null)
            return new GeneralResponse(false, "User model is empty");

        var newUser = new AppUser
        {
            FullName = userDTO.FullName,
            Address = userDTO.Address,
            Email = userDTO.Email,
            PasswordHash = userDTO.Password,
            UserName = userDTO.Email
        };

        var user = await userManager.FindByEmailAsync(userDTO.Email);
        if (user is not null)
            return new GeneralResponse(false, "User already registered.");

        var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
        if (!createUser.Succeeded)
            return new GeneralResponse(false, "Error in creating new user.. please try again.");

        var checkAdmin = await roleManager.FindByNameAsync("Admin");
        if(checkAdmin is null)
        {
            await roleManager.CreateAsync(new IdentityRole() { Name = "Admin"});
            await userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Acount Created.");
        }
        else
        {
            var checkUser = await roleManager.FindByNameAsync("User");
            if (checkUser is null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

            await userManager.AddToRoleAsync(newUser, "User");
            return new GeneralResponse(true, "Account Created.");
        }
    }

    public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
    {
        if (loginDTO == null)
            return new LoginResponse(false, null!, "Login container is empty");

        var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
        if (getUser is null)
            return new LoginResponse(false, null!, "User not found");

        bool checkUserPassword = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
        if (!checkUserPassword)
            return new LoginResponse(false, null!, "Invalid email/password");

        var getUserRole = await userManager.GetRolesAsync(getUser);
        var userSession = new UserSession(getUser.Id, getUser.FullName, getUser.Email, getUserRole.First());
        string token = GenerateToken(userSession);
        return new LoginResponse(true, token!, "Login complete");
    }

    private string GenerateToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
        };
        var token = new JwtSecurityToken(
           issuer: config["Jwt:Issuer"],
           audience: config["Jwt:Audience"],
           claims: userClaims,
           expires: DateTime.Now.AddDays(1),
           signingCredentials: credentials
           );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

