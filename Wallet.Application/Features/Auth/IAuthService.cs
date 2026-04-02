using Wallet.Application.Features.Auth.DTOs;

namespace Wallet.Application.Features.Auth;

public interface IAuthService
{
    ServiceResult Register(AuthRegisterRequest request);
    ServiceResult<AuthLoginResponse> Login(AuthLoginRequest request);
    ServiceResult Logout(AuthLogoutRequest request);
    ServiceResult<AuthLoginResponse> Refresh(AuthRefreshRequest request);

}