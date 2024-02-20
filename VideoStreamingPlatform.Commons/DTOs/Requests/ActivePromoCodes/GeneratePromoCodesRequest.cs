using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.ActivePromoCodes
{
    public class GeneratePromoCodesRequest
    {
        public int NumberOfCodes { get; set; }
        public int Balance{ get; set; }
    }
}
