using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Wallet
{
    public class EnterPromoCodeRequest
    {
        public int UserId { get; set; }

        public string CodeValue { get; set; }
    }
}
