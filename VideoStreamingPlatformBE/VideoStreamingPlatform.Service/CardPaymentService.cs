using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.CardPayment;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.CardPayment;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class CardPaymentService : ICardPaymentService
    {
        private readonly VideoStreamingPlatformContext _db;
        private readonly IConfiguration _config;
        public CardPaymentService(VideoStreamingPlatformContext dbContext, IConfiguration config)
        {
            _db = dbContext;
            _config = config;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        public CommonResponse CreateCardPayment(CreateCardPaymentRequest request)
        {
            var user= _db.Users.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (user == null)
            {
                throw new NullReferenceException("UserId provided in request does not exist.");
            }

            var options = new ChargeCreateOptions
            {
                Amount = (long)(request.Amount * 100),
                Currency = "bam",
                Description = $"Wallet recharge for user {request.UserId}",
                Source = request.StripeToken,
                Metadata = new Dictionary<string, string>
    {
        { "UserId", request.UserId.ToString() },
        { "WalletRecharge", "true" },
        { "Platform", "VideoStreamingPlatform" },
        { "Email", request.Email },
        { "PhoneNumber", request.PhoneNumber }
    }
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Status!="succeeded")
            {
                throw new Exception("Payment failed, please try again.");
            }


            //Višak? nema potrebe spašavati u db ako imamo mogućnost prosljeđivanja više podataka u naš stripe servis
            //Paypal servisom cu provjeriti da li je kartica vazeca i ako je response OK mijenjam stanje walleta datom useru
            /*var cardPayment = new CardPayment()
            {
                UserId = request.UserId,
                Amount = request.Amount,
                CardholderName = request.CardholderName,
                CardNumber = request.CardNumber,
                ExpirationDate = request.ExpirationDate,
                TransactionDate = DateTime.Now                
            };*/

            var userWallet = _db.Wallets.FirstOrDefault(x=>x.UserId == request.UserId);
            if (userWallet == null)
            {
                userWallet = new Wallet { UserId = request.UserId, Balance = request.Amount };
                _db.Wallets.Add(userWallet);
            }
            else
            {
            userWallet.Balance += request.Amount;
            }


            
            _db.SaveChanges();

            return new CommonResponse
            {
                Message = $"Wallet balance updated by {request.Amount}BAM, for user {request.UserId}"
            };
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
