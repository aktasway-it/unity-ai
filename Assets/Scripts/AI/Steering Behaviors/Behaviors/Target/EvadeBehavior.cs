using AI.Steering_Behaviors.Behaviors.Base;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Target
{
    public class EvadeBehavior : TargetSteeringBehavior
    {
        [SerializeField] private float _maxPredictionTime = 3.0f;
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 direction = _target.Position - controller.Position;
            float distance = direction.magnitude;
            float targetSpeed = _target.Velocity.magnitude;

            float predictionTime = _maxPredictionTime;
            if (targetSpeed > distance / _maxPredictionTime)
                predictionTime = distance / targetSpeed;

            Vector3 futurePosition = _target.Position + _target.Direction * targetSpeed * predictionTime;
            _steeringData.linear = (controller.Position - futurePosition) * _acceleration;
            return _steeringData;
        }
    }
}
