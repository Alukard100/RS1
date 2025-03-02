using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Wallet
{
    public class UpdateWalletRequest
    {
        public decimal? Balance { get; set; }
        public int UserId { get; set; }
    }
}
