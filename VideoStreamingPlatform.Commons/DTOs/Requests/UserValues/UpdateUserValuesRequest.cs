using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.UserValues
{
    public class UpdateUserValuesRequest
    {
        public int? UserId { get; set; }
        public string? Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }
}
