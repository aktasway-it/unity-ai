using UnityEngine;

namespace AI.Steering_Behaviors.Data
{
    public class SteeringData
    {
        public Vector3 linear;
        public Quaternion angular;

        public SteeringData()
        {
            linear = Vector3.zero;
            angular = Quaternion.identity;
        }

        public SteeringData(Vector3 linear, Quaternion angular)
        {
            this.linear = linear;
            this.angular = angular;
        }
    }
}
