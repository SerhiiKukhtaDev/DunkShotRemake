using System;
using Ball;
using Basket.Net;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Basket
{
    public class BasketInput : MonoBehaviour
    {
        [SerializeField] private BallFacade ballFacade;
        [Range(1, 10)] [SerializeField] private float force = 50;
        [Range(1, 20)] [SerializeField] private float maxForce;

        private IInputWrapper _inputWrapper;
        
        private IDisposable _updateDisposable;
        private Vector2 _startMousePosition;
        private Vector2 _dragPosition;
        private Vector2 _forceVector;
        
        private float _maxForceMagnitude;

        public float ForceDelta { get; private set; }

        [Inject]
        private void Construct(IInputWrapper inputWrapper)
        {
            _inputWrapper = inputWrapper;
        }

        private void Start()
        {
            _maxForceMagnitude = new Vector2(maxForce, maxForce).magnitude;

            ballFacade.BallMovement.InBasket.Subscribe(basket =>
            {
                _updateDisposable = Observable
                    .EveryUpdate()
                    .Subscribe(_ => UpdateWhenInBasket(ballFacade.BallMovement, ballFacade.BallPrediction, 
                        basket.Deformation, basket.Catcher)).AddTo(this);
                
            }).AddTo(this);
        }

        private void UpdateWhenInBasket(MainBallMovement mainBall, BallPrediction ballPrediction, 
            BasketDeformation basketDeformation, BallCatcher ballCatcher)
        {
            if (_inputWrapper.IsMouseDown)
                _startMousePosition = _inputWrapper.WorldMousePosition;

            if (_inputWrapper.LeftMousePressed)
            {
                ForceDelta = GetMaxForceDelta();
                _dragPosition = _inputWrapper.WorldMousePosition;
                _forceVector = GetCurrentForceVector();
            
                basketDeformation.Deform(_forceVector, ForceDelta);
                Predict(mainBall, ballPrediction, _forceVector, ForceDelta);
            }

            if (_inputWrapper.IsMouseUp)
            {
                ballCatcher.StopCatching(mainBall);
                
                MoveBall(mainBall, _forceVector);
                ballPrediction.EndPrediction();
                basketDeformation.AnimateShootAndReset().Forget();
                _updateDisposable.Dispose();
            }
        }

        private float GetMaxForceDelta()
        {
            return Mathf.Lerp(0, 1, _forceVector.magnitude / _maxForceMagnitude);
        }

        private void Predict(MainBallMovement mainBall, BallPrediction ballPrediction, Vector2 forceVector, float forceDelta)
        {
            ballPrediction.SyncPosition(mainBall.transform.position);
            MoveBall(ballPrediction.PredictionBall, forceVector);
            ballPrediction.PredictMovement(forceDelta);
        }

        private void MoveBall(BallMovement ballMovement, Vector2 forceVector)
        {
            ballMovement.Move(forceVector);
        }

        private Vector2 GetCurrentForceVector()
        {
            Vector2 ballForce = (_startMousePosition - _dragPosition) * force;
            Vector2 ballForceClamped = ballForce.Clamp(-maxForce, maxForce);

            return ballForceClamped;
        }
    }
}
