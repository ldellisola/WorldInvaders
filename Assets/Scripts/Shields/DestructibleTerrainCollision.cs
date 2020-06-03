using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Misiles;
using UnityEngine;

public class DestructibleTerrainCollision : MonoBehaviour
{

    public ArrayDestructibleTerrain ArrayTerrain;
    public DestructibleTerrain Terrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        Misile mis;

        if (obj.TryGetComponent(out mis))
        {
            if (ArrayTerrain != null && ArrayTerrain.isActiveAndEnabled)
            {
                if (CollidesWithCircle(mis.transform.position, mis.Radius, ArrayTerrain.quadTree.nodes, 0, ArrayTerrain.depth))
                {
                    mis.Explode();
                    ArrayTerrain.quadTree.Insert(mis.transform.position, mis.ExplosionRadius, false);
                }
            }
            else if (Terrain!= null && Terrain.isActiveAndEnabled)
            {
                if (CollidesWithCircle(mis.transform.position, mis.Radius, Terrain.quadTree.GetRoot()))
                {
                    mis.Explode();
                    Terrain.quadTree.Insert(mis.transform.position, mis.ExplosionRadius, false);
                }
            }




        }
    }


    public bool CollidesWithCircle(Vector2 center, float radius, QuadTree<bool>.QuadTreeNode<bool> node)
    {
        if (node.IsLeaf())
        {
            return node.Data && ContainedInCircle(center, radius, node);
        }


        foreach(var subnode in node.Nodes)
        {
            bool contained = ContainedInCircle(center, radius, subnode);

             if (contained)
                if (CollidesWithCircle(center, radius, subnode))
                    return true;
        }

        return false;
    }

    public bool ContainedInCircle(Vector2 origin, float radius, QuadTree<bool>.QuadTreeNode<bool> node)
    {

        Vector2 diff = node.Position - origin;

        diff.x = Mathf.Max(0, Mathf.Abs(diff.x) - node.Size / 2);
        diff.y = Mathf.Max(0, Mathf.Abs(diff.y) - node.Size / 2);

        return diff.sqrMagnitude < radius;

    }

    public bool CollidesWithCircle(Vector2 center, float radius, ArrayQuadTree<bool>.QuadTreeNode<bool>[] tree, int index, int depth)
    {
        if (depth == tree[index].depth)
        {
            return tree[index].Data && ContainedInCircle(center, radius, tree[index]);
        }

        int nextNode = 4 * index;

        for (int i = 1; i <= 4; i++) {
            bool contaided = ContainedInCircle(center, radius, tree[nextNode + i]);

            if (tree[nextNode + i].Data && contaided)
                return true;
            else if (!tree[nextNode + i].Data && contaided)
                return (CollidesWithCircle(center, radius, tree, nextNode + i, depth));
        }

        return false;
    }

    public bool ContainedInCircle(Vector2 origin, float radius, ArrayQuadTree<bool>.QuadTreeNode<bool> node)
    {

        Vector2 diff = node.Position - origin;

        diff.x = Mathf.Max(0, Mathf.Abs(diff.x) - node.Size / 2);
        diff.y = Mathf.Max(0, Mathf.Abs(diff.y) - node.Size / 2);

        return diff.sqrMagnitude < radius;

    }
}