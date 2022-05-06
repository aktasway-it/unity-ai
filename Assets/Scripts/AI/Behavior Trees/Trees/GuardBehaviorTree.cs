using System.Collections.Generic;
using AI.Behavior_Trees.Core;
using AI.Behavior_Trees.Nodes.Tasks;
using Objects;
using UnityEngine;

namespace AI.Behavior_Trees.Trees
{
    public class GuardBehaviorTree : BehaviorTree
    {
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Vector3[] _waypoints;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _playerLayerMask;
        protected override Node SetupTree()
        {
            transform.position = _waypoints[0];
            return new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckTargetInFOVRange(transform, 20, 60, _playerLayerMask),
                    new ChaseTarget(transform, _speed * 1.5f),
                    new AttackTarget(transform, _bullet, 20, 1)
                }),
                new Sequence(new List<Node>
                {
                    new CheckTargetInFOVRange(transform, 30, 60, _playerLayerMask),
                    new ChaseTarget(transform, _speed * 1.5f)
                }),
                new Patrol(transform, _waypoints, _speed)
            });
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < _waypoints.Length; i++)
            {
                int nextPointIndex = (i + 1) % _waypoints.Length;
                Gizmos.DrawSphere(_waypoints[i], 1.0f);
                Gizmos.DrawLine(_waypoints[i], _waypoints[nextPointIndex]);
            }
        }
    }
}
