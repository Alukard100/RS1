using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Membership
{
    public class GetMembershipRequest
    {
        public int? UserId { get; set; }

        public bool? IsActive { get; set; }

    }
}
