using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment
{
    public class GetCardPaymentRequest
    {
        public string? CardholderName { get; set; } = null!;
    }
}
