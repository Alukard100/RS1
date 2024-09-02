using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Report;
using VideoStreamingPlatform.Commons.DTOs.Requests.ReportType;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Report;
using VideoStreamingPlatform.Commons.DTOs.Responses.ReportType;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class ReportTypeService : IReportTypeService
    {
        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();

        public CommonResponse CreateReportType(CreateReportTypeRequest request)
        {
            //provjeriti da li postoje User i Video sa proslijedjenim ID u requestu.
            var videoExist = db.Videos.Where(x => x.VideoId == request.ReportId).FirstOrDefault();
            if (videoExist == null)
            {
                throw new NullReferenceException("ReportId provided in request does not exist.");
            }

            var userExist = db.Users.Where(x => x.UserId == request.ReportId).FirstOrDefault();
            if (userExist == null)
            {
                throw new NullReferenceException("ReportId provided in request does not exist.");
            }

            var newObject = new ReportType()
            {
                //ReportTypePicture = Helper.Helper.PictureHelper(request),
               ReportTypeId  = request.ReportId
            };

            var response = db.ReportTypes.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.ReportTypeId };

        }

        public CommonResponse DeleteReportType(CommonDeleteRequest request)
        {
            var removeObject = db.ReportTypes.Where(x => x.ReportTypeId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.ReportTypes.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetReportTypeResponse> GetReportTypes(GetReportTypesRequest request)
        {
            //Include?
            var response = db.ReportTypes
                .Where(x => x.ReportTypeId == request.VideoId)
                .ToList();

            var dataList = new List<GetReportTypeResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetReportTypeResponse()
                {
                    ReportId = item.ReportTypeId,
                });
            }
            return dataList;
        }

        public CommonResponse UpdateReportType(UpdateReportTypeRequest request)
        {
            var entry = db.ReportTypes.Where(x => x.ReportTypeId == request.ReportTypeId).FirstOrDefault();
            if (entry != null)
            {
                entry.ReportTypeId = request.ReportTypeId;
                db.SaveChanges();
                return new CommonResponse() { Id = request.ReportTypeId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}