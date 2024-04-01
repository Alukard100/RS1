using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment
{
    public class CreateCardPaymentRequest
    {
        public string CardNumber { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public string CardholderName { get; set; } = null!;
        public string SecurityNumber { get; set; } = null!;        
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
