using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.CardPayment;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class CardPaymentService : ICardPaymentService
    {
        //V/*ideoStreamingPlatformContext _db= new VideoStreamingPlatformContext();*/
        private readonly VideoStreamingPlatformContext _db;
        public CardPaymentService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public CommonResponse CreateCardPayment(CreateCardPaymentRequest request)
        {
            var user= _db.Users.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException("UserId provided in request does not exist.");
            }
            //Paypal servisom cu provjeriti da li je kartica vazeca i ako je response OK mijenjam stanje walleta datom useru
            var cardPayment = new CardPayment()
            {
                UserId = request.UserId,
                Amount = request.Amount,
                CardholderName = request.CardholderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                TransactionDate = DateTime.Now                
            };

            var userWallet = _db.Wallets.Where(x=>x.UserId == request.UserId).FirstOrDefault();
            userWallet.Balance += request.Amount;

            var response= _db.CardPayments.Add(cardPayment);

            _db.SaveChanges();

            return new CommonResponse() { Id = response.Entity.PaymentId, Message = $"Card payment is created for user ID> {response.Entity.UserId}" };

        }

        public List<GetCardPaymentResponse>GetCardPayment(GetCardPaymentRequest request)
        {
            var response= _db.CardPayments.ToList();

            if (!string.IsNullOrEmpty(request.CardholderName))
            {
            response= _db.CardPayments.Where(x=>x.CardholderName.Contains(request.CardholderName)).ToList();
            }

            var payments = response.ToList();

            var datalist = new List<GetCardPaymentResponse>();

            foreach (var payment in payments) {
                datalist.Add(new GetCardPaymentResponse()
                {
                    PaymentId = payment.PaymentId,
                    CardNumber = payment.CardNumber,
                    Amount = payment.Amount,
                    CardholderName = payment.CardholderName,
                    ExpirationDate = payment.ExpirationDate,
                    TransactionDate = payment.TransactionDate,
                    UserId = payment.UserId
                });                        
            }
            return datalist;

        }
    }
}
