using BlogApp.Models.Read;
using MongoDB.Driver;

namespace BlogApp.Query
{
    public class GetPostList : IQuery<IAsyncCursor<PostList>>
    {
    }

}