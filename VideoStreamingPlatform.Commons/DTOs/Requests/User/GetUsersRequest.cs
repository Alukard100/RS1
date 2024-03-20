using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.User
{
    public class GetUserRequest
    {
        public int? userID { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; } 
        public string? userName { get; set; } 

    }
}
