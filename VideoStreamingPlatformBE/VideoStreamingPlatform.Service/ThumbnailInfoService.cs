using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ThumbnailInfo;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;
using Xabe.FFmpeg;

namespace VideoStreamingPlatform.Service
{
    public class ThumbnailInfoService : IThumbnailInfoService
    {
        private readonly VideoStreamingPlatformContext _db;
        private readonly IHttpClientFactory _httpClientFactory;
        public ThumbnailInfoService(VideoStreamingPlatformContext dbContext, IHttpClientFactory httpClientFactory)
        {
            _db = dbContext;
            _httpClientFactory = httpClientFactory;
        }

        public ThumbnailInfo CreateThumbnail(int VideoId, string LocalFilePath)
        {
            if (VideoId <= 0 || string.IsNullOrEmpty(LocalFilePath) || !File.Exists(LocalFilePath))
            {
                return null;
            }
            var video = _db.Videos.FirstOrDefault(v => v.VideoId == VideoId);
            
            if (video == null) return null;

            string tempThumbnailPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid().ToString()}.jpg");
            string resizedThumbnailPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}_resized.jpg");

            try
            {
                using (var engine = new Engine())
                {
                    var inputVideo = new MediaFile { Filename = LocalFilePath };
                    var random = new Random();
                    int seekTime = Math.Max(0, random.Next(0, video.DurationInSecondes - 1));

                    var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(seekTime) };
                    var outputImage = new MediaFile { Filename = tempThumbnailPath };

                    engine.GetThumbnail(inputVideo, outputImage, options);

                    var ffmpegArgs = $"-i \"{tempThumbnailPath}\" -vf \"scale='if(gt(a,16/9),480,-2)':'if(gt(a,16/9),-2,270)'\" \"{resizedThumbnailPath}\" -y";
                    engine.CustomCommand(ffmpegArgs);

                    byte[] thumbBytes = File.ReadAllBytes(resizedThumbnailPath);

                    var newThumbnail = new ThumbnailInfo
                    {
                        VideoId = VideoId,
                        ThumbnailPicture = thumbBytes
                    };

                    _db.ThumbnailInfos.Add(newThumbnail);
                    _db.SaveChanges();

                    return newThumbnail;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error generating thumbnail: {ex.Message}");
                return null;
            }
            finally
            {
                File.Delete(tempThumbnailPath);
                File.Delete(resizedThumbnailPath);
            }
            
        }

        public bool DeleteThumbnail(ThumbnailInfo thumbnail)
        {
            if (thumbnail!=null)
            {
                _db.ThumbnailInfos.Remove(thumbnail);
                _db.SaveChanges();
                return true;
            } return true;
        }

        public ThumbnailInfo GetThumbnail(int ThumbnailId)
        {
            if (ThumbnailId <= 0)
            {
                return null;
            }
            var thumbnail = _db.ThumbnailInfos.FirstOrDefault(t => t.ThumbnailInfoId==ThumbnailId);
            if (thumbnail != null) 
            {
                return thumbnail;
            }
            return null;
        }

        public ThumbnailInfo UpdateThumbnail([FromForm] UpdateThumbnailInfoRequest request)
        {
            if (request.ThumbnailInfoId <= 0)
            {
                return null;
            }
            if (request.file == null)
            {
                return null;
            }
            var thumbnail = _db.ThumbnailInfos.FirstOrDefault(t => t.ThumbnailInfoId == request.ThumbnailInfoId);
            if (thumbnail != null)
            {
                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                var extension = Path.GetExtension(request.file.FileName).ToLower();
                if (!Array.Exists(allowedExtensions, ext => ext == extension))
                {
                    return null;
                }
                using (var ms = new MemoryStream())
                {
                    request.file.CopyTo(ms);
                    byte[] newThumbnailPicture = ms.ToArray();

                    thumbnail.ThumbnailPicture = newThumbnailPicture;

                    _db.Update(thumbnail);
                    _db.SaveChanges();

                    return thumbnail;
                } 
            }
            return null;
        }
    }
}
