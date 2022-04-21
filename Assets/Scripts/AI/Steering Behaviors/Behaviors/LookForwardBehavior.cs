using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class LookForwardBehavior : SteeringBehavior
    {
        public override SteeringData GetSteering(SteeringController controller)
        {
            _steeringData.angular = Quaternion.identity;
            if (controller.IsMoving)
            {
                Vector3 velocityDirection = controller.Velocity.normalized;
                float velocityAngle = Mathf.Atan2(velocityDirection.x, velocityDirection.z) * Mathf.Rad2Deg;
                float directionAngle = Mathf.Atan2(controller.Direction.x, controller.Direction.z) * Mathf.Rad2Deg;
                float angularDistance = Mathf.DeltaAngle(directionAngle, velocityAngle);
                angularDistance = Mathf.Clamp(angularDistance, -_acceleration, _acceleration);
                Quaternion rotation = Quaternion.AngleAxis(angularDistance, Vector3.up);
                _steeringData.angular = angularDistance != 0 ? rotation : Quaternion.identity;
            }

            return _steeringData;
        }
    }
}
