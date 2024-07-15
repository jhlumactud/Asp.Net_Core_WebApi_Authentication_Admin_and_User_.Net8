using AdminAndUserRoleAuthentication.WebApi.DTOs;
using static AdminAndUserRoleAuthentication.WebApi.DTOs.ServiceResponses;

namespace AdminAndUserRoleAuthentication.WebApi.Interfaces;

public interface IUserAccount
{
    Task<GeneralResponse> CreateAcount(UserDTO userDTO);
    Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
}
