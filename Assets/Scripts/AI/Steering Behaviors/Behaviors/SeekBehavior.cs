using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class SeekBehavior : SteeringBehavior
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected float _speed = 5.0f;
    
        public override SteeringData GetSteering(SteeringController controller)
        {
            Vector3 dirToTarget = _target.position - controller.transform.position;
            _steeringData.linear = dirToTarget.normalized * _speed;
            _steeringData.angular = Quaternion.identity;
            return _steeringData;
        }
    }
}
