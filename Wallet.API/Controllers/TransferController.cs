using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Features.Transfer;

namespace Wallet.API.Controllers;

public class TransferController(ITransferService transferService) : CustomBaseController
{
    
}