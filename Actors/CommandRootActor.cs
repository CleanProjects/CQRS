using Akka.Actor;
using BlogApp.Commands;

namespace BlogApp.Actors
{
    public class CommandRootActor : ReceiveActor
    {
        public CommandRootActor()
        {
            var savePostHandler = Context.ActorOf<SavePostHandler>();
            Receive<SavePost>(message => savePostHandler.Forward(message));
        }
    }
}