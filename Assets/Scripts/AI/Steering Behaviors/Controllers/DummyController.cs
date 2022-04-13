using UnityEngine;

namespace AI.Steering_Behaviors.Controllers
{
    public class DummyController : AIController
    {
        public override Vector3 Position => transform.position;
        public override Vector3 Direction => transform.forward;
        public override Quaternion Rotation => transform.rotation;
        public override Vector3 Velocity => Vector3.zero;
    }
}
