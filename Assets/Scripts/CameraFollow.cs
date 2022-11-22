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
    [SerializeField] private BasketInput input;
    [SerializeField] private BasketSpawner spawner;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private BallFacade ball;

    private const float MinForceDelta = 0.5f;
    private Vector3 _prevCameraPosition;
    
    private void Start()
    {
        ball.BallMovement.Moved.Subscribe(OnBallFlight).AddTo(this);
    }

    private async void OnBallFlight(Vector2 vel)
    {
        var forceDelta = input.ForceDelta;
        
        if (forceDelta <= MinForceDelta) return;
        
        _prevCameraPosition = targetCamera.transform.position;

        await UniTask.Delay(TimeSpan.FromSeconds(moveDelay));
        
        await targetCamera.transform.DOMoveY(_prevCameraPosition.y 
                                             + moveForce * forceDelta, vel.magnitude / -Physics.gravity.y - moveDelay);

        var disposable = Observable
            .EveryUpdate()
            .SkipWhile(_ => ball.transform.position.y > spawner.NextBasket.StartPoint.position.y)
            .First()
            .Subscribe(_ => targetCamera.transform.DOMoveY(_prevCameraPosition.y, returnTime))
            .AddTo(this);

        ball.BallMovement.InBasket.First().Subscribe(_ => disposable.Dispose()).AddTo(this);
    }
}
