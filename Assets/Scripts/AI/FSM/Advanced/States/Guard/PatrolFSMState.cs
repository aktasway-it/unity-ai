using UnityEngine;

namespace AI.FSM.Advanced.States.Guard
{
    public class PatrolFSMState : FSMState<GuardFSMAdvanced>
    {
        protected Vector3[] _waypoints;
        protected int _currentWaypointIndex;

        public PatrolFSMState(GuardFSMAdvanced fsm, Vector3[] waypoints) : base(fsm)
        {
            _waypoints = waypoints;
        }

        public override string StateID => "Patrol";

        public override void OnEnterState()
        {
            base.OnEnterState();
            _currentWaypointIndex = GetClosestWaypointIndex();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (Vector3.Distance(_fsm.transform.position, _waypoints[_currentWaypointIndex]) < 0.1f)
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

            if (_fsm.CanSeePlayer())
            {
                _fsm.ChangeState("Chase");
                return;
            }
        
            Vector3 dir = (_waypoints[_currentWaypointIndex] - _fsm.transform.position).normalized;
            _fsm.Move(dir);
        }
    
        private int GetClosestWaypointIndex()
        {
            int index = 0;
            float shortestDistance = float.MaxValue;
            for (int i = 0; i < _waypoints.Length; i++)
            {
                float distance = Vector3.Distance(_fsm.transform.position, _waypoints[i]);
                if (distance < shortestDistance)
                {
                    index = i;
                    shortestDistance = distance;
                }
            }

            return index;
        }
    }
}
