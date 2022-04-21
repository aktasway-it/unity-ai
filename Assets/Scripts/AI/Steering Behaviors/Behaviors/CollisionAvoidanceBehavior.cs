using System.Collections;
using System.Collections.Generic;
using AI.Steering_Behaviors.Behaviors;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

public class CollisionAvoidanceBehavior : SteeringBehavior
{
    [SerializeField] private float _predictionTime = 3.0f;
    [SerializeField] private float _collisionSphereRadius = 1.0f;
    [SerializeField] private LayerMask _collisionLayers;

    public override SteeringData GetSteering(SteeringController controller)
    {
        Vector3 direction = controller.Velocity.normalized;
        float maxDistance = controller.Velocity.magnitude;
        RaycastHit[] avoidTargetHits = Physics.SphereCastAll(controller.Position, _collisionSphereRadius, direction,
            maxDistance, _collisionLayers);
        
        _steeringData.linear = Vector3.zero;
        for (int i = 0; i < avoidTargetHits.Length; i++)
        {
            if (avoidTargetHits[i].transform.Equals(transform))
                continue;
            
            Vector3 normal = avoidTargetHits[i].normal;
            float strength = (controller.Velocity.magnitude * _predictionTime) / Mathf.Max(avoidTargetHits[i].distance, 0.1f); 
            _steeringData.linear += normal * strength * _acceleration;
        }

        return _steeringData;
    }
}