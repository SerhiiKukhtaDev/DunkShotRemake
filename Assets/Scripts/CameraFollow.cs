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
    [SerializeField] private BallFacade ball;

    private const float MinForceDelta = 0.5f;
    private Vector3 _prevCameraPosition;
    
    private void Start()
    {
        ball.BallMovement.Moved.Subscribe(OnBallFlight).AddTo(this);
    }

    private async void OnBallFlight(Vector2 vel)
    {
        var forceDelta = spawner.CurrentBasket.Input.ForceDelta;
        
        if (forceDelta <= MinForceDelta) return;
        
        _prevCameraPosition = targetCamera.transform.position;

        await UniTask.Delay(TimeSpan.FromSeconds(moveDelay));
        
        await targetCamera.transform.DOMoveY(_prevCameraPosition.y 
            + moveForce * forceDelta, vel.magnitude / -Physics.gravity.y - moveDelay);

        Observable.EveryUpdate().TakeUntil(spawner.CurrentBasket.Catcher.Caught.Merge(spawner.NextBasket.Catcher.Caught)).Subscribe(_ =>
        {
            if (ball.transform.position.y < spawner.NextBasket.StartPoint.position.y)
            {
                targetCamera.transform.DOMoveY(_prevCameraPosition.y, returnTime);
            }
        }).AddTo(this);
    }
}
