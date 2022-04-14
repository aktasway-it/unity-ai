using UnityEngine;

namespace AI.Steering_Behaviors.Controllers
{
    public abstract class AIController : MonoBehaviour
    {
        public abstract Vector3 Position { get; }
        public abstract Vector3 Direction { get; }
        public abstract Quaternion Rotation { get; }
        public abstract Vector3 Velocity { get; }
        public bool IsMoving => Velocity.sqrMagnitude > 0.01f;

        [SerializeField] protected float _maxVelocity;
        [SerializeField] protected float _drag = 1f;
    }
}
