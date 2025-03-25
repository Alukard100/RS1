using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment
{
    public class CreateCardPaymentRequest
    { 
        public string? StripeToken { get; set; }  
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

}
