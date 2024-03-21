using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.UserValues
{
    public class GetUserValuesRequest
    {
        public int? UserId { get; set; }
        public string Password { get; set; } = null!;

    }
}
