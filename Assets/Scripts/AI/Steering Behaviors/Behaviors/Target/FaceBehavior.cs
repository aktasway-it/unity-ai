using AI.Steering_Behaviors.Behaviors.Base;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Target
{
    public class FaceBehavior : TargetSteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 direction = (_target.Position - controller.Position).normalized;
            Quaternion lookAtRotation = Quaternion.LookRotation(direction);
            float yDelta = Mathf.Min(lookAtRotation.eulerAngles.y - controller.transform.eulerAngles.y, _acceleration);
            Quaternion rotation = Quaternion.Euler(0, yDelta, 0);
            _steeringData.angular = rotation.eulerAngles.y > 1 ? rotation : Quaternion.identity;
            return _steeringData;
        }
    }
}
