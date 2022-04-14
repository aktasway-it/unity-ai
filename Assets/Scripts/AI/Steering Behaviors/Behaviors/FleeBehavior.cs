using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class FleeBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = controller.Position - _target.Position;
            _steeringData.linear = dirToTarget.normalized * _acceleration;
            _steeringData.angular = Quaternion.identity;
            return _steeringData;
        }
    }
}
