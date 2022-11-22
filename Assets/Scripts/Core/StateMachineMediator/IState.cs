using System;

namespace Core.StateMachineMediator
{
    public interface IState
    {
        IObservable<TransitionParams> TransitionRequested { get; }

        void Enter(object @params = null);
        
        void Exit();
    }
}
