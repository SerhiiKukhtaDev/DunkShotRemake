using System;
using UniRx;

namespace Core.StateMachineMediator
{
    public abstract class StateBehaviour : IState
    {
        private readonly Subject<TransitionParams> _nextStateStream = new Subject<TransitionParams>();

        public IObservable<TransitionParams> TransitionRequested => _nextStateStream;

        void IState.Enter(object @params)
        {
            OnEnter();
        }

        void IState.Exit()
        {
            _nextStateStream.Dispose();
            OnExit();
        }

        protected virtual void OnExit() {}

        protected abstract void OnEnter();

        protected void GoTo<TState>(object @params = null) where TState : IState
        {
            _nextStateStream.OnNext(new TransitionParams(typeof(TState), @params));
        }
    }

    public struct TransitionParams
    {
        public TransitionParams(Type stateType, object @params = null)
        {
            StateType = stateType;
            Params = @params;
        }

        public Type StateType { get; }
        
        public object Params { get; }
    }
}
