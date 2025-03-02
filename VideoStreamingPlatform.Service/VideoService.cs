using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Video;
using VideoStreamingPlatform.Commons.DTOs.Responses.Video;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class VideoService : IVideoService
    {
        private readonly VideoStreamingPlatformContext _db;
        private readonly IVideoStatisticService _videoStatisticService;
        private readonly IRatingSystemVideoService _ratingSystemVideo;
        private readonly IThumbnailInfoService _thumbnailInfoService;
        public VideoService(VideoStreamingPlatformContext dbContext,
                            IVideoStatisticService videoStatisticService,
                            IRatingSystemVideoService ratingSystemVideo,
                            IThumbnailInfoService thumbnailInfoService) 
        {
            _db = dbContext;
            _videoStatisticService = videoStatisticService;
            _ratingSystemVideo = ratingSystemVideo;
            _thumbnailInfoService = thumbnailInfoService;
        }
        
        public Video CreateVideo(CreateVideoRequest request, string videoDirectory)
        {
            if (request.file == null || request.file.Length == 0)
            {
                return null;
            }
            var allowedExtensions = new[] { ".mp4", ".mov", ".avi" };
            var extension = Path.GetExtension(request.file.FileName).ToLower();
            if (!Array.Exists(allowedExtensions, ext => ext == extension))
            {
                return null;
            }

            if (!Directory.Exists(videoDirectory))
            {
                Directory.CreateDirectory(videoDirectory);
            }
            var uniqueFileNmae = $"{Guid.NewGuid()}_{Path.GetFileName(request.file.FileName)}";
            var filePath = Path.Combine(videoDirectory, uniqueFileNmae);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                request.file.CopyTo(stream);
            }

            var inputFile = new MediaFile { Filename = filePath };
            int durationInSeconds = 0;
            string resolution = "Unknown";

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
                durationInSeconds = (int)inputFile.Metadata.Duration.TotalSeconds;
                resolution = inputFile.Metadata.VideoData.FrameSize.ToString();
            }

            var newVideo = new Video()
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                VideoName = request.VideoName,
                FilePath = filePath,
                Description = request.Description,
                ResolutionType = resolution,
                DurationInSecondes = durationInSeconds,
                UploadDate = DateTime.UtcNow,
                IsFree = request.IsFree
            };
            
            _db.Videos.Add(newVideo);
            _db.SaveChanges();

            _videoStatisticService.CreateStatistic(newVideo.VideoId);
            _ratingSystemVideo.CreateRSV(newVideo.VideoId);
            _thumbnailInfoService.CreateThumbnail(newVideo.VideoId);
            
            return newVideo;
        }

        public bool DeleteVideo(int VideoId)
        {
            if (VideoId == null || VideoId <= 0)
            {
                return false;
            }
            Video tempVideo = _db.Videos
                            .Include(v => v.VideoStatistics)
                            .Include(v => v.RatingSystemVideos)
                            .Include(v => v.ThumbnailInfos)
                            .Where(v => v.VideoId == VideoId)
                            .FirstOrDefault();
            if (tempVideo != null) 
            {
                if (_videoStatisticService.DeleteStatistic(tempVideo.VideoStatistics) &&
                    _ratingSystemVideo.DeleteRSV(tempVideo.RatingSystemVideos) &&
                    _thumbnailInfoService.DeleteThumbnail(tempVideo.ThumbnailInfos))
                {
                    File.Delete(tempVideo.FilePath);
                    _db.Videos.Remove(tempVideo);
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public VideoResponse GetVideo(int VideoId)
        {
            var existingVideo = _db.Videos
                    .Include(v => v.Category)
                    .Where(v => v.VideoId == VideoId)
                    .Select(v => new VideoResponse
                    {
                        VideoId = v.VideoId,
                        CategoryId = v.Category.CategoryId
                    })
                    .FirstOrDefault();
            if (existingVideo != null)
            {
                return existingVideo;
            }
            return null;
        }

        public Video UpdateVideo(UpdateVideoRequest request)
        {
            var existingVideo = _db.Videos.FirstOrDefault(v => v.VideoId == request.VideoId);
            if (existingVideo != null)
            {
                existingVideo.VideoName = request.VideoName;
                existingVideo.Description = request.Description;

                _db.Update(existingVideo);
                _db.SaveChanges();
                return existingVideo;
            }
            return null;
        }
    }
}
