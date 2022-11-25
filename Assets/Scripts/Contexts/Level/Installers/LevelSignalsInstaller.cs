using Contexts.Level.Signals;
using Zenject;

namespace Contexts.Level.Installers
{
    public class LevelSignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<StarPickedSignal>();
            Container.DeclareSignal<BallHitTheBasketSignal>();
            
            Container.DeclareSignal<GamePausedSignal>();
            Container.DeclareSignal<GameResumedSignal>();
            
            Container.DeclareSignal<LoadMenuSignal>();
            Container.DeclareSignal<ReloadLevelSignal>();
            Container.DeclareSignal<GameLoseSignal>();
        }
    }
}
