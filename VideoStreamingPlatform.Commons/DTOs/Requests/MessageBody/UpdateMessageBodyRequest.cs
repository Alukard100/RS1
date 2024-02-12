using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody
{
    public class UpdateMessageBodyRequest
    {
        public int MessagebodyId { get; set; }
        public string Body { get; set; } = null!;
    }
}
