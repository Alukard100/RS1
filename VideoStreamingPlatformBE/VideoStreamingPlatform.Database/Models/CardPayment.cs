using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class CardPayment
    {
        public int PaymentId { get; set; }
        public string CardNumber { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
        public string CardholderName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
