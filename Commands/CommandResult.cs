namespace BlogApp.Commands
{
    public class CommandResult
    {
        public int Id { get; }
        public bool Successful { get; }

        public CommandResult(int id, bool successful = true)
        {
            Id = id;
            Successful = successful;
        }
    }
}