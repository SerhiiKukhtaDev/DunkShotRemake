namespace Contexts.Project.Services.Progress.Data.Transitions
{
    public class ResetScoreTransition : GameProgressTransition
    {
        public override void Execute(GameProgressReactive progress)
        {
            progress.Score.Value = 0;
        }
    }
}
