using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ActivePromoCodes;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class ActivePromoCodesService : IActivePromoCodesService
    {
        VideoStreamingPlatformContext _db = new VideoStreamingPlatformContext();

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
            Message="Promo codes generated sccessfuly."
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
