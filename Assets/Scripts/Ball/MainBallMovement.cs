using System;
using UniRx;
using UnityEngine;

namespace Ball
{
    public class MainBallMovement : BallMovement
    {
        public IObservable<Vector2> Moved => _moved;
        private readonly Subject<Vector2> _moved = new Subject<Vector2>();

        public override void Move(Vector2 force)
        {
            _moved.OnNext(force);
        
            base.Move(force);
        }

        public void ResetPhysics()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            rigidbody.rotation = 0;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void StartPhysics()
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}