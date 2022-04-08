using UnityEngine;

namespace Actors.Controllers
{
    public class ActorController : MonoBehaviour
    {
        [SerializeField] private float _speed = 3.0f;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _worldLayerMask;
    
        private Vector3 _targetPosition;
        private Vector3 _directionVector;

        private void Start()
        {
            if (_camera == null)
                _camera = Camera.main;

            _targetPosition = transform.position;
            _directionVector = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                RecalculateTargetPosition();
        
            Move();
        }

        private void Move()
        {
            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
                return;

            Vector3 dir = _targetPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 25f);
            if (dir.magnitude < _speed * Time.deltaTime)
                transform.position += dir;
            else
                transform.position += dir.normalized * _speed * Time.deltaTime;
        }

        private void RecalculateTargetPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray cameraRay = _camera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(cameraRay, out raycastHit, 200, _worldLayerMask))
            {
                _targetPosition = raycastHit.point + Vector3.up;
                _directionVector = (_targetPosition - transform.position).normalized;
            }
        }
    }
}
