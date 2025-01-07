using Microsoft.AspNetCore.Mvc;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Playlist;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Playlist;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class PlaylistService : IPlaylistService
    {
        //VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext db;
        public PlaylistService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public CommonResponse CreatePlaylist(CreatePlaylistRequest request)
        {
            var userExist= db.Users.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new NullReferenceException("User with provided ID in request does not exist.");
            }

            var newObject = new Playlist()
            {
                UserId = request.UserId,
                IsPublic=request.IsPublic,
                PlaylistName= request.PlaylistName
            };

            var response = db.Playlists.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.PlaylistId };

        }

        public CommonResponse DeletePlaylist(CommonDeleteRequest request)
        {
            var removeObject = db.Playlists.Where(x => x.PlaylistId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.Playlists.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetPlaylistResponse> GetPlaylists(GetPlaylistsRequest request)
        {
            //Method for testing purpose
            var response = db.Playlists
                .Where(x => x.UserId == request.UserId)
                .ToList();

            var dataList = new List<GetPlaylistResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetPlaylistResponse()
                {
                   PlaylistId = item.PlaylistId,
                   PlaylistName= item.PlaylistName,
                   IsPublic = item.IsPublic,
                   UserId= item.UserId
                });
            }
            return dataList;
        }

        public CommonResponse UpdatePlaylist(UpdatePlaylistRequest request)
        {
            var entry = db.Playlists.Where(x => x.PlaylistId == request.PlaylistId).FirstOrDefault();
            if (entry != null)
            {
                entry.PlaylistName=request.PlaylistName;
                entry.IsPublic=request.IsPublic;
                request.UserId = request.UserId;

                db.SaveChanges();
                return new CommonResponse() { Id = request.PlaylistId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}