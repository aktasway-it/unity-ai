using System.Collections.Generic;
using AI.Steering_Behaviors.Behaviors.Base;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

namespace AI.Steering_Behaviors.Behaviors.Movement
{
    public class PathFollowBehavior : SteeringBehavior
    {
        [SerializeField] private List<Vector3> _path;
        [SerializeField] private float _maxDistanceBeforeMovingNext = 1.0f;
        private int _currentPathIndex = 0;

        public override void Init(SteeringController controller)
        {
            _path.Insert(0, controller.Position);
        }

        public override SteeringData GetSteering(SteeringController controller)
        {
            if (Vector3.Distance(_path[_currentPathIndex], controller.Position) < _maxDistanceBeforeMovingNext)
                _currentPathIndex = (_currentPathIndex + 1) % _path.Count;
            
            Vector3 direction = (_path[_currentPathIndex] - controller.Position).normalized;
            _steeringData.linear = _acceleration * direction;
            return _steeringData;
        }

        private void OnDrawGizmosSelected()
        {
            if (_path.Count == 0)
                return;
            
            Gizmos.color = Color.green;
            for (int i = 0; i < _path.Count; i++)
            {
                Gizmos.DrawSphere(_path[i], 1.0f);
            }
        }
    }
}
