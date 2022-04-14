using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class WanderingBehavior : SteeringBehavior
    {
        [SerializeField] private float _maxRotation = 30.0f;
        [SerializeField] private float _changeRotationEvery = 2.0f;

        private float _timeToNextRotation = 0.0f;
        private float _currentRotationAngle = 0.0f;
        
        public override SteeringData GetSteering(SteeringController controller)
        {
            if (_timeToNextRotation >= _changeRotationEvery)
            {
                _timeToNextRotation = 0.0f;
                _currentRotationAngle = Random.Range(-1.0f, 1.0f) * _maxRotation;
            }
            _timeToNextRotation += Time.deltaTime;
            
            _steeringData.linear = controller.transform.forward * _acceleration;
            _steeringData.angular = Quaternion.Euler(0,  _currentRotationAngle * (_timeToNextRotation / _changeRotationEvery) * Time.deltaTime, 0);
            return _steeringData;
        }
    }
}
