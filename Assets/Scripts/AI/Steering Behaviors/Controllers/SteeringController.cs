using AI.Steering_Behaviors.Behaviors;
using UnityEngine;

namespace AI.Steering_Behaviors.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class SteeringController : MonoBehaviour
    {
        public Rigidbody Body => _rigidbody;
    
        [SerializeField] private float _maxVelocity = 10f;
        [SerializeField] private float _drag = 1f;

        public float MaxVelocity => _maxVelocity;

        private Rigidbody _rigidbody;
        private SteeringBehavior[] _steeringBehaviors;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _steeringBehaviors = GetComponents<SteeringBehavior>();
            _rigidbody.drag = _drag;
        }

        private void FixedUpdate()
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
                _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, _rigidbody.rotation * rotation, Time.time));
            else if (_rigidbody.velocity.sqrMagnitude > 0.01f)
                _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(_rigidbody.velocity), 0.75f));
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}