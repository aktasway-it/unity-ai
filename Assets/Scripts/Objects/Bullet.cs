using UnityEngine;

namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private Vector3 _velocity;

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
                Destroy(collision.collider.gameObject);
        
            Destroy(gameObject);
        }
    }
}
