using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Support;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class SupportService : ISupportService
    {
        VideoStreamingPlatformContext _db = new VideoStreamingPlatformContext();

        public CommonResponse CreateSupport(CreateSupportRequest request)
        {
            var noviSupport = new Support()
            {
                Body = request.Body,
                Seen = false,
                TimeSent = DateTime.Now,
                UserId = request.UserId
            };
            var response=_db.Supports.Add(noviSupport);
            _db.SaveChanges();
            return new CommonResponse() { Id=response.Entity.SupportId };
        }
    }
}
