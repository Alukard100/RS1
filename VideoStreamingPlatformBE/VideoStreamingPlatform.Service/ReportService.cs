using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Report;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Report;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class ReportService : IReportService
    {
        private readonly VideoStreamingPlatformContext db;
        public ReportService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public CommonResponse CreateReport(CreateReportRequest request)
        {
            //provjeriti da li postoje User i Video sa proslijedjenim ID u requestu.
            var videoExist = db.Videos.Where(x => x.VideoId == request.VideoId).FirstOrDefault();
            if (videoExist == null)
            {
                throw new NullReferenceException("VideoID provided in request does not exist.");
            }

            var userExist = db.Users.Where(x => x.UserId == request.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new NullReferenceException("UserID provided in request does not exist.");
            }

            var newObject = new Report()
            {
                //ReportPicture = Helper.Helper.PictureHelper(request),
                UserId = request.UserId,
                VideoId = request.VideoId
            };

            var response = db.Reports.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.ReportId };

        }

        public CommonResponse DeleteReport(CommonDeleteRequest request)
        {
            var removeObject = db.Reports.Where(x => x.ReportId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.Reports.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetReportResponse> GetReports(GetReportsRequest request)
        {
            //Include?
            var response = db.Reports
                .Where(x => x.UserId == request.UserId && x.VideoId == request.VideoId)
                .ToList();

            var dataList = new List<GetReportResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetReportResponse()
                {
                    ReportId = item.ReportId,
                    ReportText = item.ReportText
                });
            }
            return dataList;
        }

        public CommonResponse UpdateReport(UpdateReportRequest request)
        {
            var entry = db.Reports.Where(x => x.ReportId == request.ReportId).FirstOrDefault();
            if (entry != null)
            {
                entry.UserId = request.UserId;
                entry.VideoId = request.VideoId;
                db.SaveChanges();
                return new CommonResponse() { Id = request.ReportId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}