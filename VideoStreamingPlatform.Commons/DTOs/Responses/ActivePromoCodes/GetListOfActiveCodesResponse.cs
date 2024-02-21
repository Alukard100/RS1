using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.ActivePromoCodes
{
    public class GetListOfActiveCodesResponse
    {
        public string CodeValue { get; set; } 
        public decimal Balance { get; set; }
        public bool? IsUsed { get; set; }

    }
}
