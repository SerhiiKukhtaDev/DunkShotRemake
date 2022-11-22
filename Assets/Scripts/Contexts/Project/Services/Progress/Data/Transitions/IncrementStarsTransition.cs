namespace Contexts.Project.Services.Progress.Data.Transitions
{
    public class IncrementStarsTransition : GameProgressTransition
    {
        public override void Execute(GameProgressReactive progress)
        {
            progress.Stars.Value += 1;
        }
    }
}
