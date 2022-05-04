using System.Collections.Generic;
using UnityEngine;

namespace AI.Sensors
{
    public class SightSensor : Sensor
    {
        [SerializeField] private float _fieldOfView = 45;
        [SerializeField] private float _maxDistance = 10;
    
        protected override void UpdateSensor()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _maxDistance, _interactionLayer);
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                    continue;
            
                Vector3 dir = (collider.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dir) < _fieldOfView)
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(transform.position, dir, out raycastHit, _maxDistance);
                    LayerMask objectLayer = raycastHit.collider.gameObject.layer;
                    if (raycastHit.collider && _interactionLayer == (_interactionLayer | (1 << objectLayer)))
                        gameObjects.Add(raycastHit.collider.gameObject);
                }
            }

            TriggerSensor(gameObjects);
        }
    }
}
