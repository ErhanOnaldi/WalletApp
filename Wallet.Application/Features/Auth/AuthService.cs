using Wallet.Application.Features.Auth.DTOs;
using Wallet.Application.Interfaces.Persistence;
using Wallet.Application.Interfaces.Persistence.RefreshTokens;
using Wallet.Application.Interfaces.Persistence.Users;
using Wallet.Application.Interfaces.Token;
using Wallet.Application.Interfaces.Token.Hashing;
using Wallet.Domain.Entities;

namespace Wallet.Application.Features.Auth;

public class AuthService(ITokenService tokenService, IUnitOfWork unitOfWork, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher hasher) : IAuthService
{
    public async Task<ServiceResult> Register(AuthRegisterRequest request)
    {
        if (await userRepository.AnyAsync(x => x.Email == request.Email))
        {
            return ServiceResult.Fail("email is already registered");
        }
        var passwordHash = hasher.Hash(request.Password);
        await userRepository.AddAsync(new User()
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            FullName = request.FullName,
            IsActive = true
        });
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<AuthLoginResponse>> Login(AuthLoginRequest request)
    {
        var user =await userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return ServiceResult<AuthLoginResponse>.Fail("user not found");
        }

        if (!user.IsActive)
        {
            return ServiceResult<AuthLoginResponse>.Fail("user is not active");
        }
        
        if (!hasher.Verify(user.PasswordHash, request.Password))
        {
            return ServiceResult<AuthLoginResponse>.Fail("e mail or password did not match");
        }
        
        var token =await tokenService.GenerateJwtTokenAsync(new AuthGenerateJwtTokenRequest(user.Id, user.Email));
        var refreshToken = await tokenService.GenerateRefreshTokenAsync();
        await refreshTokenRepository.AddAsync(new RefreshToken()
        {
            Token = refreshToken,
            UserId = user.Id ,
            IsRevoked = false,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        });
        await unitOfWork.SaveChangesAsync();
        var response = new AuthLoginResponse(AccessToken: token, RefreshToken: refreshToken, AccessTokenExpiresAt: DateTime.UtcNow.AddMinutes(15));
        return ServiceResult<AuthLoginResponse>.Success(response);
    }

    public async Task<ServiceResult> Logout(AuthLogoutRequest request)
    {
        var refreshToken = await refreshTokenRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshToken == null)
        {
            return ServiceResult.Fail("refresh token not found");
        }
        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.ReasonRevoked = "Logout";
        refreshTokenRepository.Update(refreshToken);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<AuthLoginResponse>> Refresh(AuthRefreshRequest request)
    {
        var refreshToken =await refreshTokenRepository.GetByRefreshTokenAsync(request.RefreshToken);
        if (refreshToken == null)
        {
            return ServiceResult<AuthLoginResponse>.Fail("refresh token not found");
        }

        if (refreshToken.IsRevoked)
        {
            return ServiceResult<AuthLoginResponse>.Fail("refresh token invalid");
        }

        if (refreshToken.ExpiresAt < DateTime.UtcNow)
        {
            return ServiceResult<AuthLoginResponse>.Fail("refresh token expired");
        }
        var user = refreshToken.User;
        var accessToken = await tokenService.GenerateJwtTokenAsync(new AuthGenerateJwtTokenRequest(user.Id, user.Email));
        var newRefreshToken = await tokenService.GenerateRefreshTokenAsync();
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.ReasonRevoked = "Refresh";
        refreshToken.IsRevoked = true;
        refreshToken.ReplacedByToken = newRefreshToken;
        refreshTokenRepository.Update(refreshToken);
        await refreshTokenRepository.AddAsync(new RefreshToken());
        await unitOfWork.SaveChangesAsync();
        var response = new AuthLoginResponse(accessToken,newRefreshToken,DateTime.UtcNow.AddMinutes(15));
        return ServiceResult<AuthLoginResponse>.Success(response);
    }
}