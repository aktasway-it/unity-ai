using UnityEngine;

namespace AI.FSM.Advanced.States
{
    public class AttackFSMState : FSMState<GuardFSMAdvanced>
    {
        private float _attackEndDistance;
        private float _attackReloadTime;
        private float _attackReloadTimer;
    
        public AttackFSMState(GuardFSMAdvanced fsm, float attackEndDistance, float attackReloadTime) : base(fsm)
        {
            _attackEndDistance = attackEndDistance;
            _attackReloadTime = attackReloadTime;
        }

        public override string StateID => "Attack";

        public override void UpdateState()
        {
            if (!_fsm.Player)
            {
                _fsm.ChangeState("Patrol");
                return;
            }

            Vector3 playerVector = _fsm.Player.position - _fsm.transform.position;
            Vector3 dir = playerVector.normalized;

            _fsm.LookAt(dir);
        
            if (!_fsm.CanSeePlayer() || Vector3.Distance(_fsm.transform.position, _fsm.Player.position) > _attackEndDistance)
            {
                _fsm.ChangeState("Chase");
                _attackReloadTimer = _attackReloadTime;
                return;
            }
        
            if (_attackReloadTimer < _attackReloadTime)
            {
                _attackReloadTimer += Time.deltaTime;
                return;
            }

            _attackReloadTimer = 0f;
            _fsm.Shoot(dir);
        }
    }
}
