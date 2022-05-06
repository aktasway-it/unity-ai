using AI.Behavior_Trees.Core;
using UnityEngine;

namespace AI.Behavior_Trees.Nodes.Tasks
{
    public class CheckTargetInFOVRange : Node
    {
        private Transform _transform;
        private float _maxDistance;
        private float _fovAngle;
        private LayerMask _targetLayer;

        public CheckTargetInFOVRange(Transform transform, float maxDistance, float fovAngle, LayerMask targetLayer)
        {
            _transform = transform;
            _maxDistance = maxDistance;
            _fovAngle = fovAngle;
            _targetLayer = targetLayer;
        }

        public override NodeState Evaluate()
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _maxDistance, _targetLayer);
            if (colliders.Length == 0)
            {
                if (GetData("target") != null)
                {
                    Transform target = GetData("target") as Transform;
                    if (target)
                    {
                        Vector3 lastSeenTargetPosition = target.position;
                        GetRootNode().SetData("lastSeenTargetPosition", lastSeenTargetPosition);
                    }
                }
                ClearData("target");
                _state = NodeState.Failure;
                return _state;
            }

            Transform newTarget = colliders[0].transform;
            Vector3 dir = (newTarget.position - _transform.position).normalized;
            if (Vector3.Angle(_transform.forward, dir) < _fovAngle)
            {
                RaycastHit raycastHit;
                Physics.Raycast(_transform.position, dir, out raycastHit, _maxDistance);
                if (raycastHit.collider && raycastHit.collider.transform.GetInstanceID() == newTarget.GetInstanceID())
                {
                    GetRootNode().SetData("target", newTarget);
                    _state = NodeState.Success;
                    return _state;
                }
            }
            
            if (GetData("target") != null)
            {
                Transform target = GetData("target") as Transform;
                if (target)
                {
                    Vector3 lastSeenTargetPosition = target.position;
                    GetRootNode().SetData("lastSeenTargetPosition", lastSeenTargetPosition);
                }
            }
            ClearData("target");
            _state = NodeState.Failure;
            return _state;
        }
    }
}
