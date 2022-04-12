using UnityEngine;

namespace AI.Steering_Behaviors.Controllers
{
    public class ActorController : SteeringController
    {
        [SerializeField] private Transform _targetPosition;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _worldLayerMask;
        
        protected override void Start()
        {
            base.Start();
            _targetPosition.position = transform.position;
            if (_camera == null)
                _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                RecalculateTargetPosition();
        }

        private void RecalculateTargetPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray cameraRay = _camera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(cameraRay, out raycastHit, 200, _worldLayerMask))
            {
                _targetPosition.position = raycastHit.point + Vector3.up;
            }
        }
    }
}
