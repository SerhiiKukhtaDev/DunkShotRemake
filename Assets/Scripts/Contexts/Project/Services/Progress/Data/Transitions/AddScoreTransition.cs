namespace Contexts.Project.Services.Progress.Data.Transitions
{
    public class AddScoreTransition : GameProgressTransition
    {
        private readonly int _score;

        public AddScoreTransition(int score)
        {
            _score = score;
        }
        
        public override void Execute(GameProgressReactive progress)
        {
            progress.Score.Value += _score;
        }
    }
}