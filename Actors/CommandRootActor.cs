using Akka.Actor;
using BlogApp.Commands;

namespace BlogApp.Actors
{
    public class CommandRootActor : ReceiveActor
    {
        public CommandRootActor()
        {
            var savePostCommandHandler = Context.ActorOf<SavePostHandler>();
            Receive<SavePost>(message => savePostCommandHandler.Forward(message));
        }
    }
}