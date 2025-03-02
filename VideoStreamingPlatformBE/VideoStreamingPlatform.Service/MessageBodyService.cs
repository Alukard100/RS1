using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.MessageBody;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class MessageBodyService : IMessageBodyService
    {
        //VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();
        private readonly VideoStreamingPlatformContext db;
        public MessageBodyService(VideoStreamingPlatformContext dbContext)
        {
            db = dbContext;
        }

        public CommonResponse CreateMessageBody(CreateMessageBodyRequest request)
        {
            var SenderExist = db.Users.Where(x => x.UserId == request.MsgSenderId).FirstOrDefault();
            if (SenderExist==null) { throw new InvalidOperationException("Posiljalac ne postoji."); }
            var RecieverExist = db.Users.Where(x => x.UserId == request.MsgRecieverId).FirstOrDefault();
            if (RecieverExist==null) { throw new InvalidOperationException("Primalac ne postoji."); }
            

            var newObject = new MessageBody()
            {
                MsgSenderId = request.MsgSenderId,
                MsgRecieverId = request.MsgRecieverId,
                Body = request.Body,
                Seen=request.Seen,
                TimeSent=request.TimeSent,                
            };
            var response=db.MessageBodies.Add(newObject);
            db.SaveChanges();

            return new CommonResponse() { Id = response.Entity.MessagebodyId };
        }

        public CommonResponse DeleteMessageBody(CommonDeleteRequest request)
        {
            var removeObject= db.MessageBodies.Where(x=>x.MessagebodyId==request.Id).FirstOrDefault();
            if (removeObject==null) { throw new InvalidOperationException("Poruka ne postoji."); }
            db.MessageBodies.Remove(removeObject);
            db.SaveChanges();
            return new CommonResponse() { Id = request.Id };
        }
        public CommonResponse UpdateMessageBody(UpdateMessageBodyRequest request)
        {
            var updateObject=db.MessageBodies.Where(x=>x.MessagebodyId==request.MessagebodyId).FirstOrDefault();
            if (updateObject==null)
            {
             throw new InvalidOperationException("Poruka ne postoji."); 
            }
            updateObject.Body= request.Body;
            db.SaveChanges();
            return new CommonResponse() { Id = request.MessagebodyId };
        }

        public List<GetMessageBodyResponse> GetMessageBody(GetMessageBodyRequest request)
        {
            var Ucesnik1 = db.Users.Where(x => x.UserId == request.MsgRecieverId || x.UserId == request.MsgSenderId).FirstOrDefault();
            var Ucesnik2 = db.Users.Where(x => (x.UserId == request.MsgRecieverId || x.UserId == request.MsgSenderId) && x.UserId!=Ucesnik1.UserId).FirstOrDefault();
            if(Ucesnik1!=null && Ucesnik2 != null) {           

            var response = db.MessageBodies.Where(x=>(request.MsgSenderId == Ucesnik1.UserId && request.MsgRecieverId == Ucesnik2.UserId) ||
                (request.MsgSenderId== Ucesnik2.UserId && request.MsgRecieverId== Ucesnik1.UserId)).ToList();

            var dataList= new List<GetMessageBodyResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetMessageBodyResponse()
                {
                    MessagebodyId = item.MessagebodyId,
                    MsgRecieverId = item.MsgRecieverId,
                    MsgSenderId = item.MsgSenderId,
                    Body = item.Body,
                    Seen = item.Seen,
                    TimeSent = item.TimeSent
                });
            }
            return dataList;
            }
            else
            {
                throw new InvalidOperationException("Jedan od proslijedjenih korisnika ne postoji!");
            }
        }

    }
}
