using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Support;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Support;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class SupportService : ISupportService
    {
        private readonly VideoStreamingPlatformContext _db;
        public SupportService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }


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
    public CommonResponse DeleteSupport(CommonDeleteRequest request)
    {
        var supportZaBrisanje = _db.Supports.Where(x=>x.SupportId==request.Id).FirstOrDefault();
        if (supportZaBrisanje != null)
            {
                _db.Supports.Remove(supportZaBrisanje);
                _db.SaveChanges(); 
                return new CommonResponse() { Message="Support deleted.", Id = request.Id };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
    }
    public List<GetSupportResponse> GetSupport(GetSupportRequest request)
    {
            var response=_db.Supports.Where(x=>x.UserId==request.UserId).ToList();

            List<GetSupportResponse>returnLIst=new List<GetSupportResponse>();

            foreach (var item in response)
            {
                returnLIst.Add(new GetSupportResponse
                {
                    UserId=item.UserId,
                    Body=item.Body,
                    TimeSent=item.TimeSent,
                    Seen=item.Seen
                });                
            }
            return returnLIst;
    }
    }


}
