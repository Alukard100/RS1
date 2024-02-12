using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Synchronization
{
    public class GetSynchronizationResponse
    {
        public int GroupId { get; set; }
        public int SyncOwnerId { get; set; }
        public int VideoId { get; set; }
        public string GroupCode { get; set; }
    }
}
