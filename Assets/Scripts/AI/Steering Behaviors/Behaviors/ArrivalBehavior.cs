using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class ArrivalBehavior : SeekBehavior
    {
        [SerializeField] private float _thresholdDistance = 5.0f;
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.position - controller.transform.position;
            if (dirToTarget.magnitude <= _thresholdDistance)
            {
                controller.Stop();
                _steeringData.linear = Vector3.zero;
                _steeringData.angular = 0;
                return _steeringData;
            }
        
            return base.GetSteering(controller);
        }
    }
}
