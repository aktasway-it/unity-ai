using System.Collections.Generic;

namespace AI.FSM.Advanced
{
    public class FSMAdvanced<T> : FSM where T : FSMAdvanced<T>
    {
        protected FSMState<T> _currentState;
        protected Dictionary<string, FSMState<T>> _states;

        protected override void Initialize()
        {
            base.Initialize();
            _states = new Dictionary<string, FSMState<T>>();
        }

        protected override void FSMFixedUpdate()
        {
            base.FSMFixedUpdate();
            _currentState?.FixedUpdateState();
        }

        protected override void FSMUpdate()
        {
            base.FSMUpdate();
            _currentState?.UpdateState();
        }

        public void ChangeState(string newStateId)
        {
            if (_currentState != null && newStateId.Equals(_currentState.StateID))
                return;
            
            if (!_states.ContainsKey(newStateId))
                return;
            
            _currentState?.OnExitState();
            _currentState = _states[newStateId];
            _currentState.OnEnterState();
        }
    }
}
