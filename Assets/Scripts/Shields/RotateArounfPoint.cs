using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArounfPoint : MonoBehaviour
{
    public float RotateSpeed = 2f;
    public float RotateawaySpeed = 2f;
    public float Radius = 2.4f;



    public Vector2 rotation;
    public GameObject _centre;
    private float _angle;


    private void Start()
    {

    }

    private void Update()
    {


        //_angle += RotateSpeed * Time.deltaTime;

        //var offset = new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        //transform.position = _centre.transform.position + offset;



        //Vector2 direction = transform.position - _centre.transform.position;

        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);

        //transform.rotation = rotation;



        if (transform.position.x > 3 || transform.position.x < -3)
            RotateSpeed *= -1;
        var offset = new Vector3(Time.deltaTime * RotateSpeed, 0, 0);
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
}
