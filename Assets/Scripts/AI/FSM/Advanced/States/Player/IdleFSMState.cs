namespace AI.FSM.Advanced.States.Player
{
    public class IdleFSMState : FSMState<PlayerFSMAdvanced>
    {
        public IdleFSMState(PlayerFSMAdvanced fsm) : base(fsm)
        {
        }

        public override string StateID => "Idle";
    }
}
