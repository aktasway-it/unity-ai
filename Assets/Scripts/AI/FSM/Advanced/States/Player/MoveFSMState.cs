using UnityEngine;

namespace AI.FSM.Advanced.States.Player
{
    public class MoveFSMState : FSMState<PlayerFSMAdvanced>
    {
        private Transform _target;
        public MoveFSMState(PlayerFSMAdvanced fsm, Transform target) : base(fsm)
        {
            _target = target;
        }

        public override string StateID => "Move";

        public override void UpdateState()
        {
            base.UpdateState();
            Vector3 vector = _target.position - _fsm.transform.position;
            if (vector.magnitude < 0.1f)
            {
                _fsm.ChangeState("Idle");
                return;
            }
        
            _fsm.Move(vector.normalized);
        }
    }
}
