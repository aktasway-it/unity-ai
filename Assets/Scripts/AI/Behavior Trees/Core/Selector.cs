namespace AI.Behavior_Trees.Core
{
    public class Selector : Node
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
                        _state = NodeState.Success;
                        return _state;
                    case NodeState.Failure:
                        continue;
                }
            }

            _state = areChildrenStillRunning ? NodeState.Running : NodeState.Failure;
            return _state;
        }
    }
}
