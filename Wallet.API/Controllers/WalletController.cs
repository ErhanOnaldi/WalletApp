using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.Wallet;
using Wallet.Application.Features.Wallet.DTOs;

namespace Wallet.API.Controllers;
[Authorize]
public class WalletController(IWalletService walletService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllWalletList() => CreateActionResult(await walletService.GetAllWalletListAsync(GetUserId()));
    [HttpGet("{walletId}")]
    public async Task<IActionResult> GetWalletById(Guid walletId) => CreateActionResult(await walletService.GetWalletByIdAsync(walletId, GetUserId()));
    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody]WalletCreateRequest request) => CreateActionResult(await walletService.CreateWalletAsync(GetUserId(),request));
    [HttpDelete("{walletId}")]
    public async Task<IActionResult> DeleteWallet(Guid walletId) => CreateActionResult(await walletService.DeleteWalletAsync(GetUserId(), walletId));
    [HttpPatch("{walletId}")]
    public async Task<IActionResult> UpdateWallet(Guid walletId, [FromBody]WalletUpdateRequest request) => CreateActionResult(await walletService.UpdateWalletAsync(walletId, GetUserId(),request));
    [HttpPost("{walletId}/withdrawal")]
    public async Task<IActionResult> Withdrawal(Guid walletId, [FromBody]WalletDepositWithdrawalRequest request) => CreateActionResult(await walletService.WithdrawalAsync(GetUserId(), walletId,request));
    [HttpPost("{walletId}/deposit")]
    public async Task<IActionResult> Deposit(Guid walletId, [FromBody]WalletDepositWithdrawalRequest request) => CreateActionResult(await walletService.DepositAsync(GetUserId(), walletId,request));
}