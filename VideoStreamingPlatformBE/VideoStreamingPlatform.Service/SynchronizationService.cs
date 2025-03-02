using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Synchronization;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Synchronization;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly VideoStreamingPlatformContext _db;
        public SynchronizationService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }


        public CommonResponse CreateSynchronization(CreateSynchronizationRequest request)
        {
            //provjeriti da li postoje User i Video sa proslijedjenim ID u requestu.
            var videoExist = _db.Videos.Where(x => x.VideoId == request.VideoId).FirstOrDefault();
            if (videoExist == null)
            {
                throw new NullReferenceException("VideoID provided in request does not exist.");
            }

            var groupExist = _db.GroupMembers.Where(x => x.GroupId == request.GroupId).FirstOrDefault();
            if (groupExist == null)
            {
                throw new NullReferenceException("UserID provided in request does not exist.");
            }

            var newObject = new Synchronization()
            {
                //SynchronizationPicture = Helper.Helper.PictureHelper(request),
                GroupId = request.GroupId,
                GroupCode = request.GroupCode,
                SyncOwnerId = request.SyncOwnerId,
                VideoId = request.VideoId,
            };

            var response = _db.Synchronizations.Add(newObject);
            _db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.GroupId, Message = $"Synchronization is created for group ID {response.Entity.GroupId}" };

        }

        public CommonResponse DeleteSynchronization(CommonDeleteRequest request)
        {
            var removeObject = _db.Synchronizations.Where(x => x.GroupId == request.Id).FirstOrDefault();

            if (removeObject != null)
            {
                _db.Synchronizations.Remove(removeObject);
                _db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetSynchronizationResponse> GetSynchronizations(GetSynchronizationsRequest request)
        {
            //Include?
            var response = _db.Synchronizations
                .Where(x => x.SyncOwnerId == request.SyncOwnerId)
                .ToList();

            var dataList = new List<GetSynchronizationResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetSynchronizationResponse()
                {
                    GroupCode = item.GroupCode,
                    GroupId = item.GroupId,
                    VideoId = item.VideoId,
                    SyncOwnerId = item.SyncOwnerId
                });
            }
            return dataList;
        }

        public CommonResponse UpdateSynchronization(UpdateSynchronizationRequest request)
        {
            var entry = _db.Synchronizations.Where(x => x.SyncOwnerId == request.SyncOwnerId && x.GroupId == request.GroupId).FirstOrDefault();
            if (entry != null)
            {
                entry.VideoId = request.VideoId;
                entry.SyncOwnerId = request.SyncOwnerId;
                entry.GroupCode = request.GroupCode;
                entry.GroupId = request.GroupId;

                _db.SaveChanges();
                return new CommonResponse() { Id = request.SyncOwnerId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}