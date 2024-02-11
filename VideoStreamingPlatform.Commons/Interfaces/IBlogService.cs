using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Blog;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Blog;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IBlogService
    {
        CommonResponse CreateBlog(CreateBlogRequest request);
        List<GetBlogResponse> GetBlogs(GetBlogsRequest request);
        CommonResponse UpdateBlog(UpdateBlogRequest request);
        CommonResponse DeleteBlog(CommonDeleteRequest request);

    }
}
