using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class MessageBody
    {
        public int MessagebodyId { get; set; }
        public int MsgSenderId { get; set; }
        public int MsgRecieverId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime TimeSent { get; set; }
        public bool Seen { get; set; }

        public virtual User MsgReciever { get; set; } = null!;
        public virtual User MsgSender { get; set; } = null!;
    }
}
