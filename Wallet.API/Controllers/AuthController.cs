using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.Auth;
using Wallet.Application.Features.Auth.DTOs;

namespace Wallet.API.Controllers;

public class AuthController(IAuthService authService) : CustomBaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AuthRegisterRequest request) =>
        CreateActionResult(await authService.Register(request));

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]AuthLoginRequest request) => CreateActionResult(await authService.Login(request));

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody]AuthLogoutRequest request) => CreateActionResult(await authService.Logout(request));
}