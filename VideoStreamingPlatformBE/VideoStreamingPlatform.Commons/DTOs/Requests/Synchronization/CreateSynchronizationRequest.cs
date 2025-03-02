using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Synchronization
{
    public class CreateSynchronizationRequest
    {
        public int GroupId { get; set; }
        public int SyncOwnerId { get; set; }
        public int VideoId { get; set; }
        public string GroupCode { get; set; }
    }
}
