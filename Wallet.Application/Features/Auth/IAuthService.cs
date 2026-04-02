using Wallet.Application.Features.Auth.DTOs;

namespace Wallet.Application.Features.Auth;

public interface IAuthService
{
    Task<ServiceResult> Register(AuthRegisterRequest request);
    Task<ServiceResult<AuthLoginResponse>> Login(AuthLoginRequest request);
    Task<ServiceResult> Logout(AuthLogoutRequest request);
    Task<ServiceResult<AuthLoginResponse>> Refresh(AuthRefreshRequest request);

}