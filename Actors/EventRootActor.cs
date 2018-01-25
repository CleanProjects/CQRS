using Akka.Actor;
using BlogApp.Events;

namespace BlogApp.Actors
{
    public class EventRootActor : ReceiveActor
    {
        public EventRootActor()
        {
            var postListEventsHandler = Context.ActorOf<PostListEventHandler>();
            var postDetailsEventHandler = Context.ActorOf<PostDetailsEventHandler>();
            
            Receive<PostSaved>(message =>
            {
                postListEventsHandler.Forward(message);
                postDetailsEventHandler.Forward(message);
            });
        }
    }
}