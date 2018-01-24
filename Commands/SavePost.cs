using System;

namespace BlogApp.Commands
{
    public class SavePost : ICommand
    {
        public string Title { get; }
        public string Content { get; }
        public DateTime WhenCreated { get; }

        public SavePost(string title, string content)
        {
            Title = title;
            Content = content;
            WhenCreated = DateTime.Now;
        }
    }
}