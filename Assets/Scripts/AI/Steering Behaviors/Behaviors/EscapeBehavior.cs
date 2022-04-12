using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class EscapeBehavior : FleeBehavior
    {
        [SerializeField] private float _thresholdDistance = 5.0f;
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.Position - controller.transform.position;
            if (dirToTarget.magnitude >= _thresholdDistance)
            {
                controller.Stop();
                _steeringData.linear = Vector3.zero;
                _steeringData.angular = Quaternion.identity;
                return _steeringData;
            }
        
            return base.GetSteering(controller);
        }
    }
}
