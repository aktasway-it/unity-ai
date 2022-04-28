using UnityEngine;

namespace AI.FSM
{
    public abstract class FSM : MonoBehaviour
    {
        private void Start()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            FSMFixedUpdate();
        }

        private void Update()
        {
            FSMUpdate();
        }

        protected virtual void Initialize() { }
        protected virtual void FSMUpdate() { }
        protected virtual void FSMFixedUpdate() { }
    }
}
