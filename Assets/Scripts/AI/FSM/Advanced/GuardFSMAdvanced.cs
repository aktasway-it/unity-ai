using AI.FSM.Advanced.States;
using Objects;
using UnityEngine;

namespace AI.FSM.Advanced
{
    public class GuardFSMAdvanced : FSMAdvanced<GuardFSMAdvanced>
    {
        public float Speed => _speed;
        public Transform Player => _player;
    
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private Vector3[] _waypoints;
        [SerializeField] private float _attackStartDistance = 15f;
        [SerializeField] private float _attackEndDistance = 20f;
        [SerializeField] private float _attackReloadTime = 1f;
        [SerializeField] private float _bulletSpeed = 10f;
        [SerializeField] private Transform _bulletSpawner;
        [SerializeField] private Bullet _bulletPrefab;
    
        private Transform _player;

        protected override void Initialize()
        {
            base.Initialize();
            _player = GameObject.FindWithTag("Player").transform;
            _states.Add("Patrol", new PatrolFSMState(this, _waypoints));
            _states.Add("Chase", new ChaseFSMState(this, _attackStartDistance));
            _states.Add("Attack", new AttackFSMState(this, _attackEndDistance, _attackReloadTime));
            ChangeState("Patrol");
        }

        public bool CanSeePlayer()
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

        public void Move(Vector3 dir)
        {
            transform.position += dir * (_speed * Time.deltaTime);
            LookAt(dir);
        }

        public void LookAt(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }

        public void Shoot(Vector3 dir)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawner.transform.position, Quaternion.identity);
            bullet.SetVelocity(dir * _bulletSpeed);
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
