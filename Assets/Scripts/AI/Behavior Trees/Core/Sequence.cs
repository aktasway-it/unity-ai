using System;

namespace AI.Behavior_Trees.Core
{
    public class Sequence : Node
    {
        public override NodeState Evaluate()
        {
            bool areChildrenStillRunning = false;
            foreach (Node child in _children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Running:
                        areChildrenStillRunning = true;
                        continue;
                    case NodeState.Success:
                        continue;
                    case NodeState.Failure:
                        _state = NodeState.Failure;
                        return _state;
                }
            }

            _state = areChildrenStillRunning ? NodeState.Running : NodeState.Success;
            return _state;
        }
    }
}
