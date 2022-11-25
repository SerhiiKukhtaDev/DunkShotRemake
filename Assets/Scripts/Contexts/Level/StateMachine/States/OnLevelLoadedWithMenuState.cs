using Basket;
using Contexts.Level.Factories;
using Core.StateMachineMediator;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Views;

namespace Contexts.Level.StateMachine.States
{
    public class OnLevelLoadedWithMenuState : StateBehaviour
    {
        private readonly MenuView _menuView;
        private readonly LevelView _levelView;
        private readonly IInputWrapper _inputWrapper;
        private readonly BasketInput _input;
        private readonly IBallFactory _ballFactory;
        private readonly Camera _camera;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public OnLevelLoadedWithMenuState(MenuView menuView, LevelView levelView, IInputWrapper inputWrapper, 
            BasketInput input, IBallFactory ballFactory, Camera camera)
        {
            _menuView = menuView;
            _levelView = levelView;
            _inputWrapper = inputWrapper;
            _input = input;
            _ballFactory = ballFactory;
            _camera = camera;
        }
        
        protected override void OnEnter()
        {
            Observable.EveryUpdate().First(_ => !EventSystem.current.IsPointerOverGameObject() && _inputWrapper.IsMouseDown)
                .Subscribe(_ =>
                {
                    _input.StartInput(_ballFactory.Ball);
                    _camera.GetComponent<CameraFollow>().StartFollow(_input, _ballFactory.Ball);   
                    
                    HideThenShow().Forget();
                }).AddTo(_disposable);
        }
        
        private async UniTaskVoid HideThenShow()
        {
            await _menuView.Hide();
            _levelView.Show();
            
            GoTo<LevelState>();
        }
    }
}