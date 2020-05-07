using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArounfPoint : MonoBehaviour
{
    public float speed = 2f;

    
    private float Width => GetComponentInChildren<BoxCollider2D>().size.x;





    private void Start()
    {

    }

    private void Update()
    {
        if (speed > 0 && !CanMoveRight())
        {
            speed *= -1;

        }
        
        if (speed < 0 && !CanMoveLeft())
        {
            speed *= -1;
        }



        var offset = new Vector3(Time.deltaTime * speed, 0, 0);
        this.transform.position += offset;




        if (TryGetComponent<DestructibleTerrain>(out DestructibleTerrain terrain))
        {
            MoveNodes(terrain.quadTree.GetRoot(),offset);
        }




    }

    private void MoveNodes(QuadTree<bool>.QuadTreeNode<bool> node, Vector2 diff)
    {
        node.Position += diff;

        if (node.IsLeaf())
        {
            return;
        }


        foreach (var subnode in node.Nodes)
        {
            MoveNodes(subnode,diff);

        }
    }

    private bool CanMoveRight()
    {
        return CanMove(Vector2.right,Width/2);

    }

    private bool CanMoveLeft()
    {
        return CanMove(Vector2.left, Width/2 );
    }

    private bool CanMove(Vector2 direction, float size)
    {
        Vector2 a = transform.position;


        var hit = Physics2D.Raycast(a, direction, size, LayerMask.GetMask("World Border"));


        Debug.DrawRay(a,direction * size  , Color.red);

        return hit.collider == null;
    }
}
