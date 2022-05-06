using System.Collections.Generic;

namespace AI.Behavior_Trees.Core
{
    public class Selector : Node
    {
        public Selector(List<Node> children) : base(children) { }
        
        public override NodeState Evaluate()
        {
            foreach (Node node in _children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        _state = NodeState.Success;
                        return _state;
                    case NodeState.Running:
                        _state = NodeState.Running;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.Failure;
            return _state;
        }
    }
}
