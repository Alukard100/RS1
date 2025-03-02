using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ActivePromoCodes;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.ActivePromoCodes;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class ActivePromoCodesService : IActivePromoCodesService
    {
        //VideoStreamingPlatformContext _db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext _db;
        public ActivePromoCodesService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public List<GetListOfActiveCodesResponse> GetListOfActiveCodes(GetListOfActiveCodesRequest request)
        {
            var response = _db.ActivePromoCodes.Where(x=>x.IsUsed == request.IsUsed).ToList();
            var datalist=new List<GetListOfActiveCodesResponse>();
            foreach (var item in response)
            {
                datalist.Add(new GetListOfActiveCodesResponse()
                {
                    IsUsed = item.IsUsed,
                    Balance = item.Balance,
                    CodeValue=item.CodeValue
                }) ;
            }
            return datalist;
        }

        public CommonResponse DeleteUsedPromoCodes()
        {
            var promoCodesToRemove = _db.ActivePromoCodes.Where(x => x.IsUsed == true).ToList();
            if (promoCodesToRemove!=null)
            {
            foreach (var item in promoCodesToRemove)
            {
                _db.ActivePromoCodes.Remove(item);
                    _db.SaveChanges();
            }
            return new CommonResponse() { Message = "Iskoristeni promo kodovi su izbrisani." };
            }
            throw new InvalidOperationException("Nema iskoristenih promo kodova.");
        }

        public CommonResponse GeneratePromoCodes(GeneratePromoCodesRequest request)
        {
            List<ActivePromoCode> listaNovihKodova = new List<ActivePromoCode>();
            for (int i = 0; i < request.NumberOfCodes; i++)
            {
                string newCode = GenerateUniqueCode();
                ActivePromoCode newPromoCode = new ActivePromoCode() {
                    Balance = request.Balance,
                    CodeValue = newCode,
                    IsUsed = false               
                };
                listaNovihKodova.Add(newPromoCode);                
            }

            _db.AddRange(listaNovihKodova);
            _db.SaveChanges();

            return new CommonResponse() { 
            Message= "Promo codes generated successfully."
            };

        }

        private string GenerateUniqueCode()
        {
            string code=null;
            bool isUnique = false;

            
                while (!isUnique)
                {
                    // Generate a random code
                    code = GenerateRandomCode();

                    bool codeExists = _db.ActivePromoCodes.Any(c => c.CodeValue == code);
                    // Check if the code already exists in the database

                    if (!codeExists)
                    {
                        // Code is unique
                        isUnique = true;
                    }
                }
            

            return code;
        }

        private string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 20).ToUpper();
        }
    }
}
