using Akka.Actor;
using BlogApp.Query;

namespace BlogApp.Actors
{
    public class QueryRootActor : ReceiveActor
    {
        public QueryRootActor()
        {
            var postHandlerProps = Props.Create<PostHandler>();
            var postHandler = Context.ActorOf(postHandlerProps);

            Receive<GetPostDetails>(message => postHandler.Forward(message));
            Receive<GetPostList>(message => postHandler.Forward(message));
        }
    }
}