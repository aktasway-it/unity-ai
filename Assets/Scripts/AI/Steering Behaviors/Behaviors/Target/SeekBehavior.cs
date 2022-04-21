using AI.Steering_Behaviors.Behaviors.Base;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Target
{
    public class SeekBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.Position - controller.Position;
            _steeringData.linear = dirToTarget.normalized * _acceleration;
            return _steeringData;
        }
    }
}
