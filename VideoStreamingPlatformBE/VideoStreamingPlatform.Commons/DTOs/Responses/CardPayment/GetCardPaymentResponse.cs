using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.CardPayment
{
    public class GetCardPaymentResponse
    {
        public int PaymentId { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public string CardholderName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
    }
}
