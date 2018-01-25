using System.Collections.Generic;
using UnityEngine;

//ToDo: try using TileMap
public enum NodeType { empty, start, goal, slow, wall }
public class NodeManager : MonoBehaviour
{
    [SerializeField]
    GameObject nodePrefab;

    [SerializeField]
    int mapWidth;

    [SerializeField]
    int mapHeight;

    float offsetX = 0.5f;
    float offsetY = 0.5f;

    Node[,] nodes; //2D map of Nodes so I can find the adjacent nodes for a given node

    static NodeManager instance;
    public static NodeManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("NodeManager");
                instance = go.GetComponent<NodeManager>();
            }

            return instance;
        }
    }

    void Start()
    {
        //ToDo: allow this to happen more than once?
        GenerateMap();
	}

    void GenerateMap()
    {
        GameObject obj;
        Node node;
        nodes = new Node[mapWidth, mapHeight];

        float currentX = 0;
        float currentY = 0;

        //ToDo: generate a map? (find a way to prevent it from making impossible ones?)
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                obj = Instantiate(nodePrefab, new Vector3(currentX, currentY, 0), Quaternion.identity);
                node = obj.GetComponent<Node>();
                node.MapPos = new Vector2(i, j);
                nodes[i, j] = node;

                currentY += offsetY;
            }
            currentX += offsetX;
            currentY = 0;
        }
    }

    public Node GetNode(int x, int y)
    {
        //ToDo: check that the node exists and nodes is not null. 
        return nodes[x, y];
    }

    public List<Node> GetAdjacentNodes(Node n)
    {
        List<Node> adjacentNodes = new List<Node>();

        int x = (int)n.MapPos.x;
        int y = (int)n.MapPos.y;

        if (x + 1 < mapWidth)
            adjacentNodes.Add(nodes[x + 1, y]);

        if (x - 1 >= 0)
            adjacentNodes.Add(nodes[x - 1, y]);

        if (y + 1 < mapHeight)
            adjacentNodes.Add(nodes[x, y + 1]);

        if (y - 1 >= 0)
            adjacentNodes.Add(nodes[x, y - 1]);

        return adjacentNodes;
    }
}
