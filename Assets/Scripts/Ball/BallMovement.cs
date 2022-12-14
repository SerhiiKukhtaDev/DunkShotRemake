using UnityEngine;

namespace Ball
{
    public abstract class BallMovement : MonoBehaviour
    {
        [SerializeField] protected new Rigidbody2D rigidbody;

        public virtual void Move(Vector2 force)
        {
            rigidbody.angularVelocity = 100f;
            rigidbody.velocity = force;
        }
    }
}
