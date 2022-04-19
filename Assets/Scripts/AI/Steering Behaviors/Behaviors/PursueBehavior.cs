using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class PursueBehavior : TargetSteeringBehavior
    {
        [SerializeField] private float _maxPredictionTime = 3.0f;
        [SerializeField] private float _minDistance = 1.5f;
        
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 direction = _target.Position - controller.Position;
            if (direction.magnitude > _minDistance)
            {
                float distance = direction.magnitude;
                float targetSpeed = _target.Velocity.magnitude;

                float predictionTime = _maxPredictionTime;
                if (targetSpeed > distance / _maxPredictionTime)
                    predictionTime = distance / targetSpeed;

                Vector3 futurePosition = _target.Position + _target.Direction * targetSpeed * predictionTime;
                _steeringData.linear = (futurePosition - controller.Position) * _acceleration;
            }
            else
            {
                _steeringData.linear = Vector3.zero;
            }
            return _steeringData;
        }
    }
}
