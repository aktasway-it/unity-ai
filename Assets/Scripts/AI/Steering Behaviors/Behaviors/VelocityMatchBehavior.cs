using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class VelocityMatchBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 deltaVelocity = Vector3.ClampMagnitude(_target.Velocity - controller.Velocity, _acceleration);
            _steeringData.linear = deltaVelocity;
            _steeringData.angular = Quaternion.identity;
            return _steeringData;
        }
    }
}