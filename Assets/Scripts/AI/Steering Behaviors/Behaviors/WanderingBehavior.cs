using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class WanderingBehavior : SteeringBehavior
    {
        [SerializeField] private float _maxRotation = 30.0f;
        [SerializeField] private float _changeDirectionEvery = 2.0f;

        private float _timeToNextTarget = 0.0f;
        private Vector3 _currentDirectionVector;

        public override void Init(SteeringController controller)
        {
            base.Init(controller);
            _currentDirectionVector = transform.forward;
        }

        public override SteeringData GetSteering(SteeringController controller)
        {
            if (_timeToNextTarget >= _changeDirectionEvery)
            {
                _timeToNextTarget = 0.0f;
                _currentDirectionVector = Quaternion.AngleAxis(Random.Range(-1.0f, 1.0f) * _maxRotation, Vector3.up) * _currentDirectionVector;
            }
            
            _timeToNextTarget += Time.deltaTime;
            
            _steeringData.linear = _currentDirectionVector * _acceleration;
            return _steeringData;
        }
    }
}
