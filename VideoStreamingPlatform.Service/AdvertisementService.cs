using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Advertisement;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class AdvertisementService : IAdvertisementService
    {
        //VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext db;
        public AdvertisementService(VideoStreamingPlatformContext dbContext) {
            db = dbContext;
        }

        public CommonResponse CreateAdvertisement(CreateAdvertisementRequest request)
        {
            //provjeriti da li postoje User i Video sa proslijedjenim ID u requestu.
            var videoExist = db.Videos.Where(x=>x.VideoId==request.VideoId).FirstOrDefault();
            if (videoExist==null)
            {
                throw new NullReferenceException("VideoID provided in request does not exist.");
            }

            var userExist= db.Users.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new NullReferenceException("UserID provided in request does not exist.");
            }

            var newObject = new Advertisement()
            {
                //AdvertisementPicture = Helper.Helper.PictureHelper(request),
                UserId = request.UserId,
                VideoId = request.VideoId
            };

            var response = db.Advertisements.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.AdvertisementId };

        }

        public CommonResponse DeleteAdvertisement(CommonDeleteRequest request)
        {
            var removeObject = db.Advertisements.Where(x => x.AdvertisementId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.Advertisements.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetAdvertisementResponse> GetAdvertisements(GetAdvertisementsRequest request)
        {
            //Include?
            var response = db.Advertisements
                .Where(x => x.UserId == request.UserId && x.VideoId == request.VideoId)
                .ToList();

            var dataList = new List<GetAdvertisementResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetAdvertisementResponse()
                {
                    UserId = item.UserId,
                    VideoId = item.VideoId
                });
            }
            return dataList;
        }

        public CommonResponse UpdateAdvertisement(UpdateAdvertisementRequest request)
        {
            var entry = db.Advertisements.Where(x => x.AdvertisementId == request.AdvertisementId).FirstOrDefault();
            if (entry != null)
            {
                entry.AdvertisementPictureURL = request.AdvertisementPicture;
                entry.UserId = request.UserId;
                entry.VideoId = request.VideoId;
                db.SaveChanges();
                return new CommonResponse() { Id = request.AdvertisementId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}