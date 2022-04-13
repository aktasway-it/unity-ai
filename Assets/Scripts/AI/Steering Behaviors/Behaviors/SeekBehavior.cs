using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class SeekBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.Position - controller.transform.position;
            _steeringData.linear = dirToTarget.normalized * _acceleration;
            return _steeringData;
        }
    }
}
