using System;
using UnityEngine;

namespace AI.Behavior_Trees.Core
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}
