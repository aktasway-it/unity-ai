using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class LookForwardBehavior : SteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            if (controller.IsMoving)
            {
                Quaternion lookAtRotation = Quaternion.LookRotation(controller.Velocity.normalized);
                float yDelta = Mathf.Min(lookAtRotation.eulerAngles.y - controller.transform.eulerAngles.y, _acceleration);
                Quaternion rotation = Quaternion.Euler(0, yDelta, 0);
                _steeringData.angular = rotation.eulerAngles.y > 1 ? rotation : Quaternion.identity;
            }
            else
                _steeringData.angular = Quaternion.identity;
        
            return _steeringData;
        }
    }
}
