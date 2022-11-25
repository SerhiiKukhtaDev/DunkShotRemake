using System;
using Ball;
using Basket;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float moveForce = 10;
    [SerializeField] private float moveDelay = 0.2f;
    [SerializeField] private float returnTime = 0.4f;
    [SerializeField] private BasketSpawner spawner;
    [SerializeField] private Camera targetCamera;

    private const float MinForceDelta = 0.5f;
    private Vector3 _prevCameraPosition;
    private float _cameraOffset;
    private float _currentPositionWithOffset;
    
    private BasketInput _input;
    private BallFacade _ball;
    private BasketBase _nextBasket;

    private IDisposable _updateDisposable;
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    public void StartFollow(BasketInput input, BallFacade ball)
    {
        _nextBasket = spawner.NextBasket;
        _cameraOffset = spawner.Height;
        
        _ball = ball;
        _input = input;
        
        _ball.BallMovement.Moved.Subscribe(vel => OnBallFlight(vel).Forget()).AddTo(_disposable);
        
        _ball.BallMovement.InBasket.Subscribe(basket =>
        {
            if (basket.Equals(_nextBasket))
            {
                _currentPositionWithOffset += _cameraOffset;
                targetCamera.transform.DOMoveY(_currentPositionWithOffset, 0.5f);
                
                _nextBasket = spawner.NextBasket;
            }

            if (_updateDisposable != null)
            {
                _updateDisposable.Dispose();
                _updateDisposable = null;
            }
        }).AddTo(_disposable);
    }

    private async UniTaskVoid OnBallFlight(Vector2 vel)
    {
        if (vel.y <= 0) return;
        
        var forceDelta = _input.ForceDelta;
        
        if (forceDelta <= MinForceDelta) return;

        _prevCameraPosition = targetCamera.transform.position;
        var nextBasketPositionY = spawner.NextBasket.StartPoint.position.y;

        await UniTask.Delay(TimeSpan.FromSeconds(moveDelay));
        
        await targetCamera.transform
            .DOMoveY(_prevCameraPosition.y + moveForce * forceDelta, vel.magnitude / 
                -Physics.gravity.y - moveDelay).SetAutoKill();

        _updateDisposable = Observable
            .EveryUpdate()
            .SkipWhile(_ => _ball.transform.position.y > nextBasketPositionY)
            .First()
            .Subscribe(_ =>
            {
                targetCamera.transform.DOMoveY(_prevCameraPosition.y, returnTime);
            }).AddTo(_ball);
    }

    private void OnDestroy()
    {
        if (_disposable.Count > 0)
            _disposable.Dispose();

        _updateDisposable?.Dispose();
    }
}
