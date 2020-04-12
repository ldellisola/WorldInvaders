using UnityEngine;

public class DestructibleTerrain : MonoBehaviour
{



    public Material mat;

    public float size = 5;
    public int depth = 2;


    public QuadTree<bool> quadTree { get; private set; }


    // Use this for initialization
    void Awake()
    {
        quadTree = new QuadTree<bool>(transform.position, size, depth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {

        if (quadTree != null)
        {

            DrawNode(quadTree.GetRoot());
        }
    }

    private Color minColor = new Color(1, 1, 1, 1f);
    private Color maxColor = new Color(0, 0.5f, 1, 0.25f);

    private void DrawNode(QuadTree<bool>.QuadTreeNode<bool> node, int nodeDepth = 0)
    {
        if (!node.IsLeaf())
        {
            if (node.Nodes != null)
            {
                foreach (var subnode in node.Nodes)
                {
                    if (subnode != null)
                    {
                        DrawNode(subnode, nodeDepth + 1);
                    }
                }
            }
        }
        Gizmos.color = Color.Lerp(minColor, maxColor, nodeDepth / (float)depth);
        Gizmos.DrawWireCube(node.Position, new Vector3(1, 1, 0.1f) * node.Size);
    }



    void OnTriggerEnter2D(Collider2D obj)
    {
        Misile mis;

        if (obj.TryGetComponent(out mis))
        {

        }
    }




































    public float QuadTreeResolution;

    private void QuadTreeCollision(Vector3 topLeft, Vector3 botRight, Vector3 epicenter, float radius)
    {
        if ((topLeft - botRight).sqrMagnitude <= QuadTreeResolution)
        {
            print("Termino la recursion");
            return;
        }


        Vector3 topRight = new Vector2(botRight.x, topLeft.y);
        Vector3 botLeft = new Vector2(topLeft.x, botRight.y);


        // Si esta completamente adentro de 
        if ((topLeft - epicenter).sqrMagnitude > radius && (epicenter - botRight).sqrMagnitude > radius
            && (epicenter - topRight).sqrMagnitude > radius && (epicenter - botLeft).sqrMagnitude > radius)
        {
            print("Dibujo");

            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();


            GL.Begin(GL.LINES);
            GL.Color(Color.red);

            var z = new Vector3(0, 0, 5);

            GL.Vertex(topLeft + z);
            GL.Vertex(topRight + z);
            GL.Vertex(botRight + z);
            GL.Vertex(botLeft + z);

            GL.End();
            GL.PopMatrix();



            return;
        }

        // 
        if ((topLeft - epicenter).sqrMagnitude < radius)
        {
            var distanceToOther = topLeft + botRight;

            QuadTreeCollision(topLeft, distanceToOther / 2, epicenter, radius);
        }

        if ((topRight - epicenter).sqrMagnitude < radius)
        {
            var a = new Vector2((topLeft.x + topRight.x) / 2, topRight.y);
            var b = new Vector2(topRight.x, (topRight.y + botRight.y) / 2);
            QuadTreeCollision(a, b, epicenter, radius);
        }

        if ((botRight - epicenter).sqrMagnitude < radius)
        {
            var distanceToOther = topLeft + botRight;

            QuadTreeCollision(distanceToOther / 2, botRight, epicenter, radius);
        }

        if ((botLeft - epicenter).sqrMagnitude < radius)
        {
            var a = new Vector2(botLeft.x, (topLeft.y + botLeft.y) / 2);
            var b = new Vector2((botLeft.x + botRight.x) / 2, botLeft.y);
            QuadTreeCollision(a, b, epicenter, radius);
        }

    }
}
