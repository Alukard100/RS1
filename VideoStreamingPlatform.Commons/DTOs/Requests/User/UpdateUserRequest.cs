using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.User
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string? Name { get; set; } = null!;
        public string? Surname { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public int TypeId { get; set; }
    }
}
