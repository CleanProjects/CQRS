using System.Threading.Tasks;
using Akka.Actor;
using BlogApp.Models;

namespace BlogApp.Commands
{
    class SavePostHandler : ReceiveActor
    {
        public SavePostHandler()
        {
            ReceiveAsync<SavePost>(Handle);
        }

        private async Task Handle(SavePost savePost)
        {
            var record = new Post
            {
                Title = savePost.Title,
                Content = savePost.Content,
                WhenCreated = savePost.WhenCreated
            };
            
            using(var context = new MySqlDbContext())
            {
                await context.Post.AddAsync(record);
                await context.SaveChangesAsync();
            }

            //Context.System.ActorSelection("*/EventRootActor").Tell(new PersonSaved(record.Id));
            Sender.Tell(new CommandResult(record.Id), Self);
        }
    }
}