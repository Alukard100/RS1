using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.PlaylistGroup;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.PlaylistGroup;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class PlaylistGroupService : IPlaylistGroupService
    {
        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();

        public CommonResponse CreatePlaylistGroup(CreatePlaylistGroupRequest request)
        {
            var newObject = new PlaylistGroup()
            {
                PlaylistId = request.PlaylistId,
                VideoId = request.VideoId,
            };

            var response = db.PlaylistGroups.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = 1234567 };

        }

        public CommonResponse DeletePlaylistGroup(int playlistId, int videoId)
        {
            var removeObject = db.PlaylistGroups.Where(x => x.PlaylistId == playlistId && x.VideoId == videoId).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.PlaylistGroups.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = playlistId };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetPlaylistGroupResponse> GetPlaylistGroups(GetPlaylistGroupsRequest request)
        {
            //Method for testing purpose
            var response = db.PlaylistGroups.Where(x => x.PlaylistId == request.PlaylistId && x.VideoId == request.VideoId).ToList();


            var dataList = new List<GetPlaylistGroupResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetPlaylistGroupResponse()
                {
                    VideoId = item.VideoId,
                    PlaylistId = item.PlaylistId
                });
            }
            return dataList;
        }

        public CommonResponse UpdatePlaylistGroup(UpdatePlaylistGroupRequest request)
        {
            var entry = db.PlaylistGroups.Where(x => x.PlaylistId == request.PlaylistId && x.VideoId == request.VideoId).FirstOrDefault();
            if (entry != null)
            {
                request.VideoId = request.VideoId;
                request.PlaylistId = request.PlaylistId;

                db.SaveChanges();
                return new CommonResponse() { Id = request.PlaylistId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}