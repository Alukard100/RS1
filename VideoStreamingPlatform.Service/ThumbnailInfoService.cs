using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ThumbnailInfo;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;
using Xabe.FFmpeg;

namespace VideoStreamingPlatform.Service
{
    public class ThumbnailInfoService : IThumbnailInfoService
    {
        private readonly VideoStreamingPlatformContext _db;
        public ThumbnailInfoService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public ThumbnailInfo CreateThumbnail(int VideoId)
        {
            if (VideoId <= 0)
            {
                return null;
            }
            var video = _db.Videos.FirstOrDefault(v => v.VideoId == VideoId);
            if (video != null)
            {
                using (var engine = new Engine())
                {
                    var inputedVideo = new MediaFile { Filename = video.FilePath };
                    var random = new Random();
                    int randomSecond = random.Next(0, video.DurationInSecondes - 1);
                    var option = new ConversionOptions { Seek = TimeSpan.FromSeconds(randomSecond) };

                    //temporary thumbnail path
                    string thumbnailPath = Path.Combine(Path.GetDirectoryName(video.FilePath), $"{Guid.NewGuid()}.jpg");
                    var outputImage = new MediaFile { Filename = thumbnailPath };

                    engine.GetThumbnail(inputedVideo, outputImage, option);

                    byte[] tumhbBytes = File.ReadAllBytes(thumbnailPath);

                    var newThumbnail = new ThumbnailInfo
                    {
                        VideoId = VideoId,
                        ThumbnailPicture = tumhbBytes
                    };
                    _db.ThumbnailInfos.Add(newThumbnail);
                    _db.SaveChanges();

                    //clean temporary files
                    File.Delete(thumbnailPath);

                    return newThumbnail;
                }
            }
            return null;
            
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
