using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class WanderingBehavior : SteeringBehavior
    {
        [SerializeField] private float _speed = 20.0f;
        [SerializeField] private float _maxRotation = 30.0f;
        public override SteeringData GetSteering(SteeringController controller)
        {
            _steeringData.linear = controller.transform.forward * _speed;
            _steeringData.angular = controller.transform.rotation.y + Random.Range(-1.0f, 1.0f) * _maxRotation;
            return _steeringData;
        }
    }
}
