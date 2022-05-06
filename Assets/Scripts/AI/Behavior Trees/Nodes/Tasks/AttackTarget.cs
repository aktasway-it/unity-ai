using AI.Behavior_Trees.Core;
using Objects;
using UnityEngine;

namespace AI.Behavior_Trees.Nodes.Tasks
{
    public class AttackTarget : Node
    {
        private Transform _transform;
        private Bullet _bulletPrefab;
        private float _bulletSpeed;
        private float _reloadTime;

        private float _timeBeforeNextAttack;
    
        public AttackTarget(Transform transform, Bullet bulletPrefab, float bulletSpeed, float reloadTime)
        {
            _transform = transform;
            _bulletPrefab = bulletPrefab;
            _bulletSpeed = bulletSpeed;
            _reloadTime = reloadTime;
            _timeBeforeNextAttack = _reloadTime;
        }

        public override NodeState Evaluate()
        {
            Transform target = GetData("target") as Transform;
            Vector3 dir = (target.position - _transform.position).normalized;
            _transform.LookAt(target.position);
     
            _timeBeforeNextAttack += Time.deltaTime;
            if (_timeBeforeNextAttack >= _reloadTime)
            {
                _timeBeforeNextAttack = 0f;
                Bullet bullet = Object.Instantiate(_bulletPrefab, _transform.position + dir * 2, Quaternion.identity);
                bullet.SetVelocity(dir * _bulletSpeed);
            }

            _state = NodeState.Running;
            return _state;
        }
    }
}
