using BlogApp.Models.MongoDB;
using MongoDB.Driver;

namespace BlogApp.Query
{
    public class GetPostList : IQuery<IAsyncCursor<PostList>>
    {
    }

}