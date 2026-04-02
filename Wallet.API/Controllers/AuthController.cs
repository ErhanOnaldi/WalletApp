using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.API.Controllers;

public class AuthController : CustomBaseController
{
    public Task<IActionResult> Register()
    {
        throw new NotImplementedException();
    }
    public Task<IActionResult> Login()
    {
        throw new NotImplementedException();
    }
    [Authorize]
    public Task<IActionResult> Logout()
    {
        throw new NotImplementedException();
    }
}