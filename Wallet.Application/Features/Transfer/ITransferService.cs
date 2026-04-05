using Wallet.Application.Features.Transfer.DTOs;

namespace Wallet.Application.Features.Transfer;

public interface ITransferService
{
    Task<ServiceResult> TransferAsync(Guid userId, TransferRequest request);
    Task<ServiceResult<TransferGetResponse>> GetTransferById(Guid transferId, Guid userId);
    Task<ServiceResult<List<TransferGetResponse>>> GetTransfersByUser(Guid userId);
}