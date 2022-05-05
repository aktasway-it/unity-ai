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
            if (Vector3.Distance(_transform.position, _waypoints[_currentWaypointIndex]) < 0.1f)
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            
            _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_currentWaypointIndex],
                _speed * Time.deltaTime);
            _transform.LookAt(_waypoints[_currentWaypointIndex]);
            _state = NodeState.Running;
            return _state;
        }
    }
}
