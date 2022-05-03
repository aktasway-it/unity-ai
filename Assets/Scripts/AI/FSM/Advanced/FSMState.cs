namespace AI.FSM.Advanced
{
    public abstract class FSMState<T> where T : FSMAdvanced<T>
    {
        public abstract string StateID { get; }
        protected T _fsm;
        
        private FSMState() {}
        
        public FSMState(T fsm)
        {
            _fsm = fsm;
        }
        
        public virtual void OnEnterState() { }
        public virtual void OnExitState() { }
        public virtual void FixedUpdateState() { }
        public virtual void UpdateState() { }
    }
}
