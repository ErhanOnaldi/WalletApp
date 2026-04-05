using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.Transfer;
using Wallet.Application.Features.Transfer.DTOs;

namespace Wallet.API.Controllers;

public class TransferController(ITransferService transferService) : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest request) =>
        CreateActionResult(await transferService.TransferAsync(GetUserId(), request));
    [HttpGet("{transferId}")]
    public async Task<IActionResult> GetTransfer(Guid transferId) => CreateActionResult( await transferService.GetTransferById(transferId, GetUserId()));
    [HttpGet]
    public async Task<IActionResult> GetTransfers() => CreateActionResult(await transferService.GetTransfersByUser(GetUserId()));
}