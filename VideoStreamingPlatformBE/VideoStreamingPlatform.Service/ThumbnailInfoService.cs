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

        public ThumbnailInfo CreateThumbnail(int VideoId)
        {
            if (VideoId <= 0)
            {
                return null;
            }
            var video = _db.Videos.FirstOrDefault(v => v.VideoId == VideoId);
            
            if (video == null) return null;

            string videoUrl = video.FilePath;
            if (!Uri.IsWellFormedUriString(videoUrl, UriKind.Absolute))
            {
                return null;
            }

            string tempVideoPath = DownloadVideoLocally(videoUrl);
            if (string.IsNullOrEmpty(tempVideoPath)) { return null; }
            
            string tempThumbnailPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");

            try
            {
                using (var engine = new Engine())
                {
                    var inputedVideo = new MediaFile { Filename = tempVideoPath };
                    var random = new Random();
                    int randomSecond = random.Next(0, video.DurationInSecondes - 1);
                    var option = new ConversionOptions { Seek = TimeSpan.FromSeconds(randomSecond) };

                    //temporary thumbnail path
                    var outputImage = new MediaFile { Filename = tempThumbnailPath };

                    engine.GetThumbnail(inputedVideo, outputImage, option);

                    byte[] tumhbBytes = File.ReadAllBytes(tempThumbnailPath);

                    var newThumbnail = new ThumbnailInfo
                    {
                        VideoId = VideoId,
                        ThumbnailPicture = tumhbBytes
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
                //clean temporary files
                File.Delete(tempVideoPath);
                File.Delete(tempThumbnailPath);
            }           
            
        }

        public string DownloadVideoLocally(string videoUrl)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = client.GetAsync(videoUrl).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                string tempVideoPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.mp4");
                using (var fs = new FileStream(tempVideoPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    response.Content.CopyToAsync(fs).GetAwaiter().GetResult();
                }

                return tempVideoPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading video: " + ex.Message);
                return null;
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
