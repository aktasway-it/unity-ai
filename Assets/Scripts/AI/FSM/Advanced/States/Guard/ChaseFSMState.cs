using UnityEngine;

namespace AI.FSM.Advanced.States.Guard
{
    public class ChaseFSMState : FSMState<GuardFSMAdvanced>
    {
        private float _attackStartDistance; 
        public ChaseFSMState(GuardFSMAdvanced fsm, float attackStartDistance) : base(fsm)
        {
            _attackStartDistance = attackStartDistance;
        }

        public override string StateID => "Chase";

        public override void UpdateState()
        {
            base.UpdateState();
            if (!_fsm.CanSeePlayer())
            {
                _fsm.ChangeState("Patrol");
                return;
            }
        
            if (Vector3.Distance(_fsm.transform.position, _fsm.Player.position) < _attackStartDistance)
            {
                _fsm.ChangeState("Attack");
                return;
            }

            Vector3 dir = (_fsm.Player.position - _fsm.transform.position).normalized;
            _fsm.Move(dir);
        }
    }
}
