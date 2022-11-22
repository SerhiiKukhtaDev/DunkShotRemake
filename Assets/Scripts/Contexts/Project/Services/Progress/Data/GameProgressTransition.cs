namespace Contexts.Project.Services.Progress.Data
{
    public abstract class GameProgressTransition
    {
        public abstract void Execute(GameProgressReactive progress);
    }
}
