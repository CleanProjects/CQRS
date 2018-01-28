using BlogApp.Models.Read;

namespace BlogApp.Query
{
    public class GetPostDetails : IQuery<PostDetails>
    {
        public int Id { get; }

        public GetPostDetails(int id)
        {
            Id = id;
        }
    }
}