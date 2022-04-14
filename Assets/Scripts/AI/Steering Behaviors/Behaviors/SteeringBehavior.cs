using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public abstract class SteeringBehavior : MonoBehaviour
    {
        [SerializeField] protected float _acceleration;
        protected SteeringData _steeringData = new SteeringData();
        
        public virtual void Init(SteeringController controller) { }
        public abstract SteeringData GetSteering(SteeringController controller);
    }
}