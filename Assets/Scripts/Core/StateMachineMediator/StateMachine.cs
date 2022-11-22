using System;
using UniRx;
using Zenject;

namespace Core.StateMachineMediator
{
    public abstract class StateMachine : IInitializable, IDisposable
    {
        private readonly DiContainer _container;
        private StateDisposable _currentState;

        protected StateMachine(DiContainer container)
        {
            _container = container;
        }

        void IInitializable.Initialize()
        {
            Initialize();
        }

        void IDisposable.Dispose()
        {
            _currentState?.Dispose();
            Dispose();
        }
        
        protected abstract void Initialize();

        protected virtual void Dispose() { }

        protected void ChangeState<TType>() where TType : IState
        {
            ChangeState(new TransitionParams(typeof(TType)));
        }

        private void ChangeState(TransitionParams transitionParams)
        {
            _currentState?.Dispose();

            IState nextState = (IState)_container.Instantiate(transitionParams.StateType);
            IDisposable disposable = nextState.TransitionRequested.Subscribe(ChangeStateInternal);
            _currentState = new StateDisposable(nextState, disposable);
            
            nextState.Enter(transitionParams.Params);
        }

        private void ChangeStateInternal(TransitionParams stateType)
        {
            ChangeState(stateType);
        }
        
        private class StateDisposable : IDisposable
        {
            private readonly IState _currentState;
            private readonly IDisposable _disposable;

            public StateDisposable(IState state, IDisposable disposable)
            {
                _currentState = state;
                _disposable = disposable;
            }
        
            public void Dispose()
            {
                _disposable.Dispose();
                _currentState.Exit();
            }
        }
    }
}
