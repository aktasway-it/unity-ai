using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class AlignBehavior : SteeringBehavior
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected float _angularSpeed = 90.0f;

        public override SteeringData GetSteering(SteeringController controller)
        {
            float yDelta = Mathf.Min(_target.transform.rotation.eulerAngles.y - controller.transform.eulerAngles.y, _angularSpeed);
            Quaternion rotation = Quaternion.Euler(0, yDelta, 0);
            _steeringData.angular = rotation.eulerAngles.y > 1 ? rotation : Quaternion.identity;
            return _steeringData;
        }
    }
}
