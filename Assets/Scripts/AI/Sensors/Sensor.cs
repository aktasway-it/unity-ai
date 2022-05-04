using System.Collections.Generic;
using UnityEngine;

namespace AI.Sensors
{
    public abstract class Sensor : MonoBehaviour
    {
        public delegate void SensorTriggered(List<GameObject> triggerObjects);
        public event SensorTriggered onSensorTriggered;
    
        [SerializeField] protected LayerMask _interactionLayer;
        [SerializeField] protected float _updateInterval;
        [SerializeField] protected bool _usePhysicsUpdate;

        protected float _updateTimer;
    
        protected virtual void Initialize() { }
        protected abstract void UpdateSensor();

        private void Start()
        {
            Initialize();
        }

        protected void FixedUpdate()
        {
            if (!_usePhysicsUpdate)
                return;

            _updateTimer += Time.deltaTime;
            if (_updateTimer >= _updateInterval)
            {
                _updateTimer = 0f;
                UpdateSensor();
            }
        }
    
        protected void Update()
        {
            if (_usePhysicsUpdate)
                return;

            _updateTimer += Time.deltaTime;
            if (_updateTimer >= _updateInterval)
            {
                _updateTimer = 0f;
                UpdateSensor();
            }
        }

        protected void TriggerSensor(List<GameObject> gameObjects)
        {
            onSensorTriggered?.Invoke(gameObjects);
        }
    }
}
