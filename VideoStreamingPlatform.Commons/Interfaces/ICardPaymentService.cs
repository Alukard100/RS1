using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.CardPayment;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ICardPaymentService
    {
        CommonResponse CreateCardPayment(CreateCardPaymentRequest request);
        List<GetCardPaymentResponse> GetCardPayment(GetCardPaymentRequest request);

    }
}
