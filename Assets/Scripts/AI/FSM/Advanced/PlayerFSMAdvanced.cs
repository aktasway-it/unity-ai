using AI.FSM.Advanced.States.Player;
using UnityEngine;

namespace AI.FSM.Advanced
{
    public class PlayerFSMAdvanced : FSMAdvanced<PlayerFSMAdvanced>
    {
        [SerializeField] private float _speed = 10.0f;
        [SerializeField] private Transform _target;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _worldLayerMask;

        protected override void Initialize()
        {
            base.Initialize();
            _target.position = transform.position;
            if (_camera == null)
                _camera = Camera.main;
        
            _states.Add("Idle", new IdleFSMState(this));
            _states.Add("Move", new MoveFSMState(this, _target));
            ChangeState("Idle");
        }

        protected override void FSMUpdate()
        {
            base.FSMUpdate();
            if (Input.GetMouseButtonDown(0))
            {
                RecalculateTargetPosition();
                ChangeState("Move");
            }
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
    
        private void RecalculateTargetPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray cameraRay = _camera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(cameraRay, out raycastHit, 200, _worldLayerMask))
            {
                _target.position = raycastHit.point + Vector3.up;
            }
        }
    }
}
