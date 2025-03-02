using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Wallet
    {
        public int WalletId { get; set; }
        public decimal? Balance { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
