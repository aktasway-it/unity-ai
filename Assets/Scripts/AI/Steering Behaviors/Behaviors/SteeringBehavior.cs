using System;
using System.Collections;
using System.Collections.Generic;
using AI.Steering_Behaviors.Controllers;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors
{
    public class SteeringData
    {
        public Vector3 linear;
        public float angular;

        public SteeringData()
        {
            linear = Vector3.zero;
            angular = 0;
        }

        public SteeringData(Vector3 linear, float angular)
        {
            this.linear = linear;
            this.angular = angular;
        }
    }

    public abstract class SteeringBehavior : MonoBehaviour
    {
        protected SteeringData _steeringData = new SteeringData();
        public abstract SteeringData GetSteering(SteeringController controller);
    }
}