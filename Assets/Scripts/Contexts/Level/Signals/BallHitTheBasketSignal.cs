namespace Contexts.Level.Signals
{
    public struct BallHitTheBasketSignal
    {
        public int Combo { get; }

        public BallHitTheBasketSignal(int combo)
        {
            Combo = combo;
        }
    }
}
