using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class ArrivalBehavior : SeekBehavior
    {
        [SerializeField] private float _thresholdDistance = 5.0f;
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.Position - controller.Position;
            if (dirToTarget.magnitude <= _thresholdDistance)
            {
                controller.Stop();
                _steeringData.linear = Vector3.zero;
                return _steeringData;
            }
        
            return base.GetSteering(controller);
        }
    }
}
