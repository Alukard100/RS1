using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Wallet;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Wallet;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IWalletService
    {
        CommonResponse UpdateWallet(UpdateWalletRequest request);
        GetWalletResponse GetWallet(GetWalletRequest request);
    }
}
