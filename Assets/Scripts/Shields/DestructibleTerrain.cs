using UnityEngine;

public class DestructibleTerrain : MonoBehaviour
{

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

}
