using AI.Steering_Behaviors.Behaviors.Base;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Target
{
    public class VelocityMatchBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 deltaVelocity = Vector3.ClampMagnitude(_target.Velocity - controller.Velocity, _acceleration);
            _steeringData.linear = deltaVelocity;
            return _steeringData;
        }
    }
}
