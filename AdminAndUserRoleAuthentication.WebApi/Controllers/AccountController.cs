using AdminAndUserRoleAuthentication.WebApi.DTOs;
using AdminAndUserRoleAuthentication.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminAndUserRoleAuthentication.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        #region
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await userAccount.CreateAcount(userDTO);
            return Ok(response);
        }
        #endregion

        #region
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }
        #endregion
    }
}
