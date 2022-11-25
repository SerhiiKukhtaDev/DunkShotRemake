using Basket;
using Contexts.Level.Factories;
using Core.StateMachineMediator;
using UnityEngine;
using Views;

namespace Contexts.Level.StateMachine.States
{
    public class OnLevelReloadedState : StateBehaviour
    {
        private readonly MenuView _menuView;
        private readonly LevelView _levelView;
        private readonly BasketInput _input;
        private readonly IBallFactory _ballFactory;
        private readonly Camera _camera;

        public OnLevelReloadedState(MenuView menuView, LevelView levelView, 
            BasketInput input, IBallFactory ballFactory, Camera camera)
        {
            _menuView = menuView;
            _levelView = levelView;
            _input = input;
            _ballFactory = ballFactory;
            _camera = camera;
        }
        
        protected override void OnEnter()
        {
            _input.StartInput(_ballFactory.Ball);
            _camera.GetComponent<CameraFollow>().StartFollow(_input, _ballFactory.Ball);

            _menuView.HideInstant();
            _levelView.Show();
            
            GoTo<LevelState>();
        }
    }
}