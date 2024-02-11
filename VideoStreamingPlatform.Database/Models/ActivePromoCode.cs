using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class ActivePromoCode
    {
        public int PromoCodeId { get; set; }
        public string CodeValue { get; set; } = null!;
        public bool? IsUsed { get; set; }
        public decimal Balance { get; set; }
    }
}
