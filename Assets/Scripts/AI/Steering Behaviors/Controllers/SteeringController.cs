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
            float rotation = 0f;

            foreach (SteeringBehavior steeringBehavior in _steeringBehaviors)
            {
                SteeringData steeringData = steeringBehavior.GetSteering(this);
                velocity += steeringData.linear * Time.deltaTime;
                rotation += steeringData.angular * Time.deltaTime;
            }
        
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity + velocity, _maxVelocity);
            if (rotation != 0)
                _rigidbody.rotation *= Quaternion.Euler(0, rotation, 0);
            else if (_rigidbody.velocity.sqrMagnitude > 0.01f)
                _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(_rigidbody.velocity), 0.75f);
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}