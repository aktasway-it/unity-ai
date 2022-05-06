using System.Collections.Generic;

namespace AI.Behavior_Trees.Core
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    
    public abstract class Node
    {
        public Node parent;
        protected NodeState _state;
        protected List<Node> _children = new List<Node>();
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        public abstract NodeState Evaluate();
        public void SetData(string key, object value) => _data[key] = value;

        public object GetData(string key)
        {
            object value = null;
            if (_data.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;

                node = node.parent;
            }

            return null;
        }
        
        public bool ClearData(string key)
        {
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }

        protected Node GetRootNode()
        {
            Node parentNode = parent;
            while (parentNode != null)
            {
                if (parentNode.parent == null)
                    return parentNode;

                parentNode = parentNode.parent;
            }

            return parentNode;
        }
        
        private void Attach(Node node)
        {
            node.parent = this;
            _children.Add(node);
        }
    }
}
