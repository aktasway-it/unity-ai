using AI.Behavior_Trees.Core;
using UnityEngine;

namespace AI.Behavior_Trees.Nodes.Tasks
{
    public class ChaseTarget : Node
    {
        private Transform _transform;
        private float _speed;

        public ChaseTarget(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            object target = GetData("target");
            if (target == null)
            {
                _state = NodeState.Failure;
                return _state;
            }
            
            Transform targetTransform = target as Transform;
            if (Vector3.Distance(_transform.position, targetTransform.position) > 0.1f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, targetTransform.position,
                    _speed * Time.deltaTime);
                _transform.LookAt(targetTransform.position);
            }

            _state = NodeState.Running;
            return _state;
        }
    }
}
