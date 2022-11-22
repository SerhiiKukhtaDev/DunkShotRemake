using Basket;

namespace Contexts.Level.Signals
{
    public struct BallHitTheBasketSignal
    {
        public int Combo { get; }
        
        public BasketBonusChecker Basket { get; }

        public BallHitTheBasketSignal(int combo, BasketBonusChecker basket)
        {
            Combo = combo;
            Basket = basket;
        }
    }
}
