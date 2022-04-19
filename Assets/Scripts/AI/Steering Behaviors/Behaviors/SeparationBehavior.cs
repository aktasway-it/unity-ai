using System.Collections;
using System.Collections.Generic;
using AI.Steering_Behaviors.Behaviors;
using AI.Steering_Behaviors.Controllers;
using AI.Steering_Behaviors.Data;
using UnityEngine;

public class SeparationBehavior : SteeringBehavior
{
    [SerializeField] private int _physicsMaxAgentCast = 10;
    [SerializeField] private float _physicsAgentCastRadius = 2.0f;
    [SerializeField] private float _decayCoefficient = 2.0f;
    [SerializeField] private LayerMask _agentsLayer;
    private Collider[] _agents;

    public override void Init(SteeringController controller)
    {
        base.Init(controller);
        _agents = new Collider[_physicsMaxAgentCast];
    }

    public override SteeringData GetSteering(SteeringController controller)
    {
        int agentsInRange =
            Physics.OverlapSphereNonAlloc(controller.Position, _physicsAgentCastRadius, _agents, _agentsLayer);

        _steeringData.linear = Vector3.zero;

        for (int i = 0; i < agentsInRange; i++)
        {
            if (!_agents[i].CompareTag("AIAgentsGroup"))
                continue;
            
            Vector3 direction = controller.Position - _agents[i].transform.position;
            float distance = direction.magnitude;
            float strength = Mathf.Min(_decayCoefficient / (distance * distance), _acceleration);
            _steeringData.linear += strength * direction.normalized;
        }

        return _steeringData;
    }
}