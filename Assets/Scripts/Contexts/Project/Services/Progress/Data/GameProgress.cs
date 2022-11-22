using System;
using UniRx;

namespace Contexts.Project.Services.Progress.Data
{
    public class GameProgressReactive
    {
        public IntReactiveProperty Score { get; }

        public IntReactiveProperty Stars { get; }

        public IntReactiveProperty MaxScore { get; }

        public GameProgressReactive(GameProgress progress)
        {
            Score = new IntReactiveProperty(0);
            Stars = new IntReactiveProperty(progress.Stars);
            MaxScore = new IntReactiveProperty(progress.MaxScore);
        }
    }

    public static class GameProgressExtension
    {
        public static GameProgress ToSerializable(this GameProgressReactive progress)
        {
            return new GameProgress() { Stars = progress.Stars.Value, MaxScore = progress.MaxScore.Value };
        }
    }

    [Serializable]
    public class GameProgress
    {
        public int Stars { get; set; }
        
        public int MaxScore { get; set; }
    }
}
