using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.MessageBody
{
    public class GetMessageBodyResponse
    {
        public int MessagebodyId { get; set; }
        public int MsgSenderId { get; set; }
        public int MsgRecieverId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime TimeSent { get; set; }
        public bool Seen { get; set; }
    }
}
