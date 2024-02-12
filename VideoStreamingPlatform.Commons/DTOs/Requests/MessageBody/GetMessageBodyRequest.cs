using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody
{
    public class GetMessageBodyRequest
    {
        public int MsgSenderId { get; set; }
        public int MsgRecieverId { get; set; }
    }
}
