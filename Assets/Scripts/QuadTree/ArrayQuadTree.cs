using System;
using System.Collections.Generic;
using UnityEngine;

//public enum QuadTreeIndex
//{
//    TopLeft = 0,
//    TopRight = 1,
//    BottomRight = 2,
//    BottomLeft = 3
//}

public class ArrayQuadTree<TType> where TType :  IComparable
{
    private readonly QuadTreeNode<TType>[] nodes;
    public int depth { get; private set; }

    public event EventHandler QuadTreeUpdated;
    public ArrayQuadTree(Vector2 position, float size, int depth)
    {
        this.depth = depth;
        nodes = BuildQuadtree(position, size, depth);
    }

    private QuadTreeNode<TType>[] BuildQuadtree(Vector2 position, float size, int depth)
    {
        int lenght = (int) (Mathf.Pow(4, depth + 1) - 1) / 3;

        var tree = new QuadTreeNode<TType>[lenght];

        // Creo manualmente al primer elemento
        tree[0] = new QuadTreeNode<TType>(position, size, 0);

        BuildQuadtreeRecursive(tree, 0);

        return tree;
    }

    private void BuildQuadtreeRecursive(QuadTreeNode<TType>[] tree, int index)
    {
        if (tree[index].depth >= this.depth)
            return;

        int nextNode = 4 * index ;

        Vector2 deltaX = new Vector2(tree[index].Size / 4,0);
        Vector2 deltaY = new Vector2(0, tree[index].Size / 4);

        tree[nextNode + 1] = new QuadTreeNode<TType>(tree[index].Position - deltaX + deltaY, tree[index].Size/4,tree[index].depth + 1);
        tree[nextNode + 2] = new QuadTreeNode<TType>(tree[index].Position + deltaX + deltaY, tree[index].Size/4,tree[index].depth + 1);
        tree[nextNode + 3] = new QuadTreeNode<TType>(tree[index].Position - deltaX - deltaY, tree[index].Size/4,tree[index].depth + 1);
        tree[nextNode + 4] = new QuadTreeNode<TType>(tree[index].Position + deltaX - deltaY, tree[index].Size/4,tree[index].depth + 1);

        BuildQuadtreeRecursive(tree, nextNode + 1);
        BuildQuadtreeRecursive(tree, nextNode + 2);
        BuildQuadtreeRecursive(tree, nextNode + 3);
        BuildQuadtreeRecursive(tree, nextNode + 4);

    }

    
    internal void Insert(Vector2 origin, float radius, TType value)
    {
        var leafNodes = new LinkedList<QuadTreeNode<TType>>();
        CircleSearch(leafNodes, origin, radius, nodes, 0);

        foreach(var leaf in leafNodes)
        {
            leaf.Data = value;
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



    public void CircleSearch(LinkedList<QuadTreeNode<TType>> selectedNodes, Vector2 targetPosition, float radius, QuadTreeNode<TType>[] tree, int index)
    {
        if (this.depth == tree[index].depth)
        {
            selectedNodes.AddLast(tree[index]);
            return;
        }

        int nextNode = 4 * index;

        for (int i = 1; i <= 4; ++i)
            if (ContainedInCircle(targetPosition, radius, tree[nextNode + i]))
                CircleSearch(selectedNodes, targetPosition, radius,tree,nextNode + i);

    }
    private bool ContainedInCircle(Vector2 origin, float radius, QuadTreeNode<TType> node)
    {

        Vector2 diff = node.Position - origin;

        diff.x = Mathf.Max(0, Mathf.Abs(diff.x) - node.Size / 2);
        diff.y = Mathf.Max(0, Mathf.Abs(diff.y) - node.Size / 2);

        return diff.sqrMagnitude < radius;

    }

    internal IEnumerable<QuadTreeNode<TType>> GetLeafNodes()
    {
        int leafNodes = (int)Mathf.Pow(4, depth);

        for (int i = nodes.Length - leafNodes; i < nodes.Length; i++)
        {
            yield return nodes[i];
        }
    }


    public class QuadTreeNode<TType> where TType : IComparable
    {
        Vector2 position;
        float size;
        QuadTreeNode<TType>[] subNodes;
        public TType Data { get; internal set; }

        public int depth;

        public QuadTreeNode(Vector2 pos, float size, int depth, TType value = default(TType))
        {
            position = pos;
            this.size = size;
            this.Data = value;
            this.depth = depth;
        }


        public Vector2 Position
        {
            get { return position; }
        }

        public float Size
        {
            get { return size; }
        }

        
    }
}



