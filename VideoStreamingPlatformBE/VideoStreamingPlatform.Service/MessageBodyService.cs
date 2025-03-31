using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.MessageBody;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoStreamingPlatform.Service
{
    public class MessageBodyService : IMessageBodyService
    {
        private readonly VideoStreamingPlatformContext db;

        public MessageBodyService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public async Task<CommonResponse> CreateMessageBody(CreateMessageBodyRequest request)
        {
            var senderExists = await db.Users.FirstOrDefaultAsync(x => x.UserId == request.MsgSenderId);
            var receiverExists = await db.Users.FirstOrDefaultAsync(x => x.UserId == request.MsgRecieverId);

            if (senderExists == null) throw new InvalidOperationException("Pošiljalac ne postoji.");
            if (receiverExists == null) throw new InvalidOperationException("Primalac ne postoji.");

            var newMessage = new MessageBody
            {
                MsgSenderId = request.MsgSenderId,
                MsgRecieverId = request.MsgRecieverId,
                Body = request.Body,
                Seen = false,
                TimeSent = DateTime.UtcNow
            };

            await db.MessageBodies.AddAsync(newMessage);
            await db.SaveChangesAsync();

            return new CommonResponse { Id = newMessage.MessagebodyId };
        }

        public async Task<CommonResponse> DeleteMessageBody(CommonDeleteRequest request)
        {
            var message = await db.MessageBodies.FindAsync(request.Id);
            if (message == null) throw new InvalidOperationException("Poruka ne postoji.");

            db.MessageBodies.Remove(message);
            await db.SaveChangesAsync();

            return new CommonResponse { Id = request.Id };
        }

        public async Task<CommonResponse> UpdateMessageBody(UpdateMessageBodyRequest request)
        {
            var message = await db.MessageBodies.FindAsync(request.MessagebodyId);
            if (message == null) throw new InvalidOperationException("Poruka ne postoji.");

            message.Body = request.Body;
            await db.SaveChangesAsync();

            return new CommonResponse { Id = request.MessagebodyId };
        }

        public async Task<List<GetMessageBodyResponse>> GetMessageBody(GetMessageBodyRequest request)
        {
            var user1 = await db.Users.FirstOrDefaultAsync(x => x.UserId == request.MsgRecieverId || x.UserId == request.MsgSenderId);
            var user2 = await db.Users.FirstOrDefaultAsync(x => (x.UserId == request.MsgRecieverId || x.UserId == request.MsgSenderId) && x.UserId != user1.UserId);

            if (user1 == null || user2 == null)
                throw new InvalidOperationException("Jedan od proslijeđenih korisnika ne postoji!");

            var messages = await db.MessageBodies
                .Where(x =>
                    (x.MsgSenderId == user1.UserId && x.MsgRecieverId == user2.UserId) ||
                    (x.MsgSenderId == user2.UserId && x.MsgRecieverId == user1.UserId))
                .ToListAsync();

            return messages.Select(m => new GetMessageBodyResponse
            {
                MessagebodyId = m.MessagebodyId,
                MsgRecieverId = m.MsgRecieverId,
                MsgSenderId = m.MsgSenderId,
                Body = m.Body,
                Seen = m.Seen,
                TimeSent = m.TimeSent
            }).ToList();
        }
    }
}
