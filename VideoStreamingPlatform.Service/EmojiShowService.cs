using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.EmojiShow;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.EmojiShow;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class EmojiShowService : IEmojiShowService
    {
        //VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext db;
        public EmojiShowService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public CommonResponse CreateEmojiShow(CreateEmojiShowRequest request)
        {
            var newObject = new EmojiShow()
            {
                //EmojiShowPicture = Helper.Helper.PictureHelper(request),
                EmojiName = request.EmojiName,
            };

            var response = db.EmojiShows.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.EmojiShowId };

        }

        public CommonResponse DeleteEmojiShow(CommonDeleteRequest request)
        {
            var removeObject = db.EmojiShows.Where(x => x.EmojiShowId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.EmojiShows.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetEmojiShowResponse> GetEmojiShows(GetEmojiShowsRequest request)
        {
            //Method for testing purpose
            var response = db.EmojiShows
                .Where(x => x.EmojiShowId == request.EmojiShowId)
                .ToList();

            var dataList = new List<GetEmojiShowResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetEmojiShowResponse()
                {
                    EmojiName = item.EmojiName,
                    EmojiShowId = item.EmojiShowId
                });
            }
            return dataList;
        }

        public CommonResponse UpdateEmojiShow(UpdateEmojiShowRequest request)
        {
            var entry = db.EmojiShows.Where(x => x.EmojiShowId == request.EmojiShowId).FirstOrDefault();
            if (entry != null)
            {
                entry.EmojiName = request.EmojiName;

                db.SaveChanges();
                return new CommonResponse() { Id = request.EmojiShowId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}