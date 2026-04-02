using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Interfaces.Persistence;
using Wallet.Application.Interfaces.Token;

namespace Wallet.Application.Features.Auth;

public class AuthService(ITokenService tokenService, IUnitOfWork unitOfWork) : IAuthService
{
    public ServiceResult Register(AuthRegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public ServiceResult<AuthLoginResponse> Login(AuthLoginRequest request)
    {
        throw new NotImplementedException();
    }

    public ServiceResult Logout(AuthLogoutRequest request)
    {
        throw new NotImplementedException();
    }

    public ServiceResult<AuthLoginResponse> Refresh(AuthRefreshRequest request)
    {
        throw new NotImplementedException();
    }
}