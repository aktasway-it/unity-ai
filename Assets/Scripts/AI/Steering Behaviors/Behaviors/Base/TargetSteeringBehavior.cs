using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Base
{
    public abstract class TargetSteeringBehavior : SteeringBehavior
    {
        [SerializeField] protected AIController _target;

        public void SetTarget(AIController target)
        {
            _target = target;
        }
    }
}
