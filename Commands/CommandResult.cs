namespace BlogApp.Commands
{
    public class CommandResult
    {
        public bool Successful { get; }

        public CommandResult(bool successful = true)
        {
            Successful = successful;
        }
    }
}