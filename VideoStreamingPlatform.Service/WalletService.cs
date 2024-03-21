using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Wallet;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Wallet;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class WalletService : IWalletService
    {
        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        public CommonResponse UpdateWallet(UpdateWalletRequest request)
        {
            var updateObject=db.Wallets.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (updateObject!=null)
            {
                updateObject.Balance = request.Balance;
                db.SaveChanges();
                return new CommonResponse() { Id = request.UserId,Message="Vas novcanik je izmijenjen." };
            }
            else
            {
            throw new InvalidOperationException("Taj wallet ne postoji.");
            }
        }
        public CommonResponse EnterPromoCode(EnterPromoCodeRequest request)
        {
            var checkPromoCode= db.ActivePromoCodes.Where(x=>x.CodeValue==request.CodeValue).FirstOrDefault();
            var userWallet= db.Wallets.Where(x=> x.UserId==request.UserId).FirstOrDefault();
            if (checkPromoCode != null && checkPromoCode.IsUsed==false)
            {
                userWallet.Balance += checkPromoCode.Balance;
                checkPromoCode.IsUsed=true;
                db.SaveChanges();
                return new CommonResponse() { Message = $"Promo kod od {checkPromoCode.Balance} coina uspjesno aktiviran." };
            }
            else { throw new InvalidOperationException("Kod koji ste unijeli je nevazeci."); }
        }

        public GetWalletResponse GetWallet(GetWalletRequest request)
        {
            var response = new GetWalletResponse();
            var getWallet= db.Wallets.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (getWallet!=null)
            {
                response.Balance = getWallet.Balance;
            }
            else
            {
                throw new InvalidOperationException("Taj wallet ne postoji.");
            }

            return response;
        }
    }
}
