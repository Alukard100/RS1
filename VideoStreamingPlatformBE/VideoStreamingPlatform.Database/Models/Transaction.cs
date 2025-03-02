using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateOfTransaction { get; set; }
        public decimal TransactionAmount { get; set; }
        public bool TransactionStatus { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
