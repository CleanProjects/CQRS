namespace BlogApp.Events
{
    public class PostSaved : IEvent
    {
        public int Id { get; }

        public PostSaved(int id)
        {
            Id = id;
        }
    }
}