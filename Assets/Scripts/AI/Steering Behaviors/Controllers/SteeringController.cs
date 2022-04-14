using AI.Steering_Behaviors.Behaviors;
using UnityEngine;

namespace AI.Steering_Behaviors.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class SteeringController : AIController
    {
        public override Vector3 Position => _rigidbody.position;
        public override Vector3 Direction => transform.forward;
        public override Quaternion Rotation => _rigidbody.rotation;
        public override Vector3 Velocity => _rigidbody.velocity;

        protected Rigidbody _rigidbody;
        protected SteeringBehavior[] _steeringBehaviors;

        protected virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _steeringBehaviors = GetComponents<SteeringBehavior>();
            _rigidbody.drag = _drag;
        }

        protected virtual void FixedUpdate()
        {
            Vector3 velocity = Vector3.zero;
            Quaternion rotation = Quaternion.identity;

            foreach (SteeringBehavior steeringBehavior in _steeringBehaviors)
            {
                SteeringData steeringData = steeringBehavior.GetSteering(this);
                velocity += steeringData.linear;
                rotation *= steeringData.angular;
            }
        
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity + velocity, _maxVelocity);
            if (rotation != Quaternion.identity)
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rigidbody.rotation * rotation, Time.time);
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}