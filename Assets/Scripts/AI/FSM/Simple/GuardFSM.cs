using System;
using Objects;
using UnityEngine;

namespace AI.FSM.Simple
{
    public class GuardFSM : FSM
    {
        public enum FSMState
        {
            Patrol,
            Chase,
            Attack
        }
    
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Vector3[] _waypoints;
        [SerializeField] private float _attackStartDistance = 15f;
        [SerializeField] private float _attackEndDistance = 20f;
        [SerializeField] private float _attackReloadTime = 1f;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private Transform _bulletSpawner;
        [SerializeField] private Bullet _bullet;
    
        private FSMState _currentState = FSMState.Patrol;
        private int _currentWaypointIndex;
        private Transform _player;
        private float _attackReloadTimer;

        protected override void Initialize()
        {
            base.Initialize();
            transform.position = _waypoints[0];
            _attackReloadTimer = _attackReloadTime;
            _player = GameObject.FindWithTag("Player").transform;
        }

        protected override void FSMUpdate()
        {
            base.FSMUpdate();
            switch (_currentState)
            {
                case FSMState.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMState.Chase:
                    UpdateChaseState();
                    break;
                case FSMState.Attack:
                    UpdateAttackState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdatePatrolState()
        {
            if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex]) < 0.1f)
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

            if (CanSeePlayer())
            {
                _currentState = FSMState.Chase;
                return;
            }
        
            Vector3 dir = (_waypoints[_currentWaypointIndex] - transform.position).normalized;
            transform.position += dir * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dir);
        }
    
        private void UpdateChaseState()
        {
            if (!CanSeePlayer())
            {
                _currentState = FSMState.Patrol;
                _currentWaypointIndex = GetClosestWaypointIndex();
                return;
            }
        
            if (Vector3.Distance(transform.position, _player.position) < _attackStartDistance)
            {
                _currentState = FSMState.Attack;
                return;
            }

            Vector3 dir = (_player.position - transform.position).normalized;
            transform.position += dir * (_speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(dir);
        }

        private void UpdateAttackState()
        {
            if (!_player)
            {
                _currentState = FSMState.Patrol;
                _currentWaypointIndex = GetClosestWaypointIndex();
                return;
            }

            Vector3 playerVector = _player.position - transform.position;
            Vector3 dir = playerVector.normalized;

            transform.rotation = Quaternion.LookRotation(dir);
        
            if (!CanSeePlayer() || Vector3.Distance(transform.position, _player.position) > _attackEndDistance)
            {
                _currentState = FSMState.Chase;
                _attackReloadTimer = _attackReloadTime;
                return;
            }
        
            if (_attackReloadTimer < _attackReloadTime)
            {
                _attackReloadTimer += Time.deltaTime;
                return;
            }

            _attackReloadTimer = 0f;
            Bullet bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity);
            bullet.SetVelocity(dir * _bulletSpeed);
        }

        private int GetClosestWaypointIndex()
        {
            int index = 0;
            float shortestDistance = float.MaxValue;
            for (int i = 0; i < _waypoints.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, _waypoints[i]);
                if (distance < shortestDistance)
                {
                    index = i;
                    shortestDistance = distance;
                }
            }

            return index;
        }

        private bool CanSeePlayer()
        {
            if (!_player)
                return false;
        
            Vector3 dir = (_player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir) < 60)
            {
                RaycastHit raycastHit;
                Physics.Raycast(transform.position + dir, dir, out raycastHit, 100);
                return raycastHit.collider && raycastHit.collider.CompareTag("Player");
            }

            return false;
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