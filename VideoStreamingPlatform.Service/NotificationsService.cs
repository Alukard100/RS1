using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Notifications;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Notifications;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class NotificationsService : INotificationService
    {
        //VideoStreamingPlatformContext _db= new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext _db;
        public NotificationsService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }
        public CommonResponse CreateNotification(CreateNotificationRequest request)
        {
            var sender= _db.Users.Where(x=>x.UserId==request.SenderUserId).FirstOrDefault();
            var recipient= _db.Users.Where(x=>x.UserId==request.RecipientUserId).FirstOrDefault();

            if (sender==null || recipient==null)
            {
                throw new InvalidOperationException("Sender or recipient with provided ID does not exist.");
            }

            var notification = new Notification()
            {
                IsRead = false,
                CreatedAt = DateTime.Now,
                NotificationTypeId=request.NotificationTypeId,
                RecipientUserId=request.RecipientUserId,
                SenderUserId=request.SenderUserId                
            };
            
            var response= _db.Notifications.Add(notification);

            _db.SaveChanges();

            return new CommonResponse() { Id = response.Entity.NotificationId, Message = "Notification successfully sent." };

        }

        public CommonResponse DeleteNotification(CommonDeleteRequest request)
        {
            var notification = _db.Notifications.Where(x => x.NotificationId == request.Id).FirstOrDefault();
            if (notification == null) { throw new InvalidOperationException("Notification with provided ID does not exist."); }
            _db.Notifications.Remove(notification);
            _db.SaveChanges();
            return new CommonResponse() { Id = notification.NotificationId, Message = "Notification has been deleted." };
        }

        public List<GetNotificationsResponse> GetNotification(GetNotificationsRequest request)
        {
            var notifications = _db.Notifications.Where(x=>x.RecipientUserId==request.RecipientUserId).ToList();

            if (request.NotificationTypeId != null)
            {
                notifications=notifications.Where(x=>x.NotificationTypeId==request.NotificationTypeId).ToList();
            }
            if (request.IsRead != null)
            {
                notifications=notifications.Where(x=>x.IsRead==request.IsRead).ToList();
            }

            var datalist=new List<GetNotificationsResponse>();

            foreach (var notification in notifications)
            {
                datalist.Add(new GetNotificationsResponse() { 
                RecipientUserId=notification.RecipientUserId,
                SenderUserId=notification.SenderUserId,
                CreatedAt=notification.CreatedAt,
                NotificationTypeId=notification.NotificationTypeId,
                IsRead=notification.IsRead
                });
            }

            return datalist;
        }
    }
}
