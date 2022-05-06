using AI.Behavior_Trees.Core;
using UnityEngine;

namespace AI.Behavior_Trees.Nodes.Tasks
{
    public class Patrol : Node
    {
        private Transform _transform;
        private Vector3[] _waypoints;
        private float _speed;
        
        private int _currentWaypointIndex;

        public Patrol(Transform transform, Vector3[] waypoints, float speed)
        {
            _transform = transform;
            _waypoints = waypoints;
            _speed = speed;
        }
        
        public override NodeState Evaluate()
        {
            object lastSeenTargetPosition = GetData("lastSeenTargetPosition");
            if (lastSeenTargetPosition != null)
            {
                Vector3 lastSeenTargetPositionVector3 = lastSeenTargetPosition is Vector3 ? (Vector3)lastSeenTargetPosition : default;
                if (Vector3.Distance(_transform.position, lastSeenTargetPositionVector3) < 0.1f)
                {
                    _currentWaypointIndex = GetClosestWaypointIndex();
                    ClearData("lastSeenTargetPosition");
                }
                else
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, lastSeenTargetPositionVector3,
                        _speed * Time.deltaTime);
                    _transform.LookAt(lastSeenTargetPositionVector3);
                }
            }
            else
            {
                if (Vector3.Distance(_transform.position, _waypoints[_currentWaypointIndex]) < 0.1f)
                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            
                _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_currentWaypointIndex],
                    _speed * Time.deltaTime);
                _transform.LookAt(_waypoints[_currentWaypointIndex]);
            }
            
            _state = NodeState.Running;
            return _state;
        }
        
        private int GetClosestWaypointIndex()
        {
            int index = 0;
            float shortestDistance = float.MaxValue;
            for (int i = 0; i < _waypoints.Length; i++)
            {
                float distance = Vector3.Distance(_transform.position, _waypoints[i]);
                if (distance < shortestDistance)
                {
                    index = i;
                    shortestDistance = distance;
                }
            }

            return index;
        }
    }
}
