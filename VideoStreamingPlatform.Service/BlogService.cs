using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Blog;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Blog;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class BlogService : IBlogService
    {
        VideoStreamingPlatformContext db = new VideoStreamingPlatformContext();

        public CommonResponse CreateBlog(CreateBlogRequest request)
        {
            var userExist= db.Users.Where(x=>x.UserId==request.UserId).FirstOrDefault();
            if (userExist == null)
            {
                throw new NullReferenceException("User with provided ID in request does not exist.");
            }

            var newObject = new Blog()
            {
                //BlogPicture = Helper.Helper.PictureHelper(request),
                UserId = request.UserId,
                Content=request.Content,
                Picture=request.Picture,
                Title = request.Title
            };

            var response = db.Blogs.Add(newObject);
            db.SaveChanges();
            return new CommonResponse() { Id = response.Entity.BlogId };

        }

        public CommonResponse DeleteBlog(CommonDeleteRequest request)
        {
            var removeObject = db.Blogs.Where(x => x.BlogId == request.Id).FirstOrDefault();

            //provjera da li objekat sa proslijedjenim ID postoji u bazi.
            if (removeObject != null)
            {
                db.Blogs.Remove(removeObject);
                db.SaveChanges();
                return new CommonResponse() { Id = request.Id };
            }

            throw new NullReferenceException("Object with provided ID does not exist.");
        }

        public List<GetBlogResponse> GetBlogs(GetBlogsRequest request)
        {
            //Method for testing purpose
            var response = db.Blogs
                .Where(x => x.UserId == request.UserId)
                .ToList();

            var dataList = new List<GetBlogResponse>();

            foreach (var item in response)
            {
                dataList.Add(new GetBlogResponse()
                {
                    UserId = item.UserId,
                    Content=item.Content,
                    Picture=item.Picture,
                    Title=item.Title
                });
            }
            return dataList;
        }

        public CommonResponse UpdateBlog(UpdateBlogRequest request)
        {
            var entry = db.Blogs.Where(x => x.BlogId == request.BlogId).FirstOrDefault();
            if (entry != null)
            {
                entry.Picture= request.Picture;
                entry.UserId = request.UserId;
                entry.Content=request.Content;
                entry.Title= request.Title;

                db.SaveChanges();
                return new CommonResponse() { Id = request.BlogId };
            }
            throw new NullReferenceException("Object with provided ID does not exist.");
        }
    }
}