using System;
using System.Collections.Generic;
using UnityEngine;

public enum QuadTreeIndex
{
    TopLeft = 0,
    TopRight = 1,
    BottomRight = 2,
    BottomLeft = 3
}

public class QuadTree<TType>
{
    private QuadTreeNode<TType> node;
    private int depth;

    public event EventHandler QuadTreeUpdated;
    public QuadTree(Vector2 position, float size, int depth)
    {
        node = new QuadTreeNode<TType>(position, size, depth);
        this.depth = depth;
    }

    public void Insert(Vector2 pos, TType value)
    {
        var leafNode = node.Subdivide(pos, value, depth );
        leafNode.value = value;
        NotifyQuadtreeUpdate();
    }

    internal void Insert(Vector2 origin, float radius, TType value)
    {
        var leafNodes = new LinkedList<QuadTreeNode<TType>>();
        node.CircleSubdivide(leafNodes, origin, radius, value, depth);

        foreach(var leaf in leafNodes)
        {
            leaf.value = value;
        }

        NotifyQuadtreeUpdate();
    }

    // Esta funcion sirve para avisarle al mesh generator que  tiene que redibujarse
    private void NotifyQuadtreeUpdate()
    {
        if (this.QuadTreeUpdated != null)
        {
            QuadTreeUpdated(this, new EventArgs());
        }
    }


    private static int GetIndexOfPosition(Vector2 lookupPosition, Vector2 nodePosition)
    {
        int index = 0;

        index |= lookupPosition.y < nodePosition.y ? 2 : 0;
        index |= lookupPosition.x > nodePosition.x ? 1 : 0;

        return index;
    }

    public QuadTreeNode<TType> GetRoot()
    {
        return node;
    }

    public IEnumerable<QuadTreeNode<TType>> GetLeafNodes()
    {
        return node.GetLeafNodes();
    }
    public class QuadTreeNode<TType>
    {
        Vector2 position;
        float size;
        QuadTreeNode<TType>[] subNodes;
        public TType value { get; internal set; }

        public int depth;

        public QuadTreeNode(Vector2 pos, float size, int depth, TType value = default(TType))
        {
            position = pos;
            this.size = size;
            this.value = value;
            this.depth = depth;
        }

        public IEnumerable<QuadTreeNode<TType>> Nodes
        {
            get { return subNodes; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public float Size
        {
            get { return size; }
        }

        public QuadTreeNode<TType> Subdivide(Vector2 targetPosition, TType type, int depth = 0)
        {
            if (depth == 0)
            {
                this.value = value;
                return this;
            }

            var subdivIndex = GetIndexOfPosition(targetPosition, position);

            if (subNodes == null)
            {
                subNodes = new QuadTreeNode<TType>[4];

                for (int i = 0; i < subNodes.Length; ++i)
                {
                    Vector2 newPos = position;
                    // Y axis
                    if ((i & 2) == 2)
                    {
                        newPos.y -= size * 0.25f;
                    }
                    else
                    {
                        newPos.y += size * 0.25f;
                    }

                    // X axis
                    if ((i & 1) == 1)
                    {
                        newPos.x += size * 0.25f;
                    }
                    else
                    {
                        newPos.x -= size * 0.25f;
                    }

                    subNodes[i] = new QuadTreeNode<TType>(newPos, size * 0.5f, depth-1);

                }
            }

            return subNodes[subdivIndex].Subdivide(targetPosition, type, depth - 1);



        }
        public void CircleSubdivide(LinkedList<QuadTreeNode<TType>> selectedNodes,Vector2 targetPosition,float radius, TType type, int depth = 0)
        {
            if (depth == 0)
            {
                this.value = value;
                selectedNodes.AddLast(this);
                return;
            }

            var subdivIndex = GetIndexOfPosition(targetPosition, position);

            if (subNodes == null)
            {
                subNodes = new QuadTreeNode<TType>[4];

                for (int i = 0; i < subNodes.Length; ++i)
                {
                    Vector2 newPos = position;
                    // Y axis
                    if ((i & 2) == 2)
                    {
                        newPos.y -= size * 0.25f;
                    }
                    else
                    {
                        newPos.y += size * 0.25f;
                    }

                    // X axis
                    if ((i & 1) == 1)
                    {
                        newPos.x += size * 0.25f;
                    }
                    else
                    {
                        newPos.x -= size * 0.25f;
                    }

                    subNodes[i] = new QuadTreeNode<TType>(newPos, size * 0.5f, depth - 1);

                }
            }

            for (int i = 0; i < subNodes.Length; ++i)
            {
                if (subNodes[i].ContainedInCircle(targetPosition, radius))
                {
                    subNodes[i].CircleSubdivide(selectedNodes, targetPosition, radius, type, depth - 1);
                }
            }


        }
        private bool ContainedInCircle(Vector2 origin, float radius)
        {

            Vector2 diff = this.position - origin;

            diff.x = Mathf.Max(0, Mathf.Abs(diff.x) - size / 2);
            diff.y = Mathf.Max(0, Mathf.Abs(diff.y) - size / 2);

            return diff.sqrMagnitude< radius;

        }

        public bool IsLeaf()
        {
            return depth == 0;
        }

        internal IEnumerable<QuadTreeNode<TType>> GetLeafNodes()
        {
            if (IsLeaf())
            {
                yield return this;
            }
            else
            {
                if (subNodes != null)
                {
                    foreach (var node in subNodes)
                    {
                        foreach (var a in node.GetLeafNodes())
                        {
                            yield return a;
                        }
                    }
                }
            }
        }
    }
}



