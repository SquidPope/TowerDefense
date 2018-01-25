using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    bool walkable = true;
    int cost = 1;

    Vector2 mapPos;

    //A*
    public int f = 0;
    public int g = 0;
    public int h = 0;

    NodeType type = NodeType.empty;

    List<Node> adjacentNodes;
    Node previousNode;

    Renderer renderer;

    public bool Walkable
    {
        get { return walkable; }
        set
        {
            if (walkable == value)
                return;

            walkable = value;
            if (walkable)
                Type = NodeType.empty;
            else
                Type = NodeType.wall;
        }
    }

    public int Cost
    {
        get { return cost; }
        set
        {
            cost = value;
            if (cost > 1)
            {
                Type = NodeType.slow;
            }
            else
            {
                Type = NodeType.empty;
            }
        }
    }

    public NodeType Type
    {
        get { return type; }
        set
        {
           type = value;
            ChangeColorByType();
        }
    }

    public Vector2 MapPos
    {
        get { return mapPos; }
        set { mapPos = value; }
    }

    public Node PreviousNode
    {
        get { return previousNode; }
        set { previousNode = value; }
    }

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        //ChangeColor(false, false, false);

        adjacentNodes = new List<Node>();
    }

    //Trying out lazy loading
    public List<Node> AdjacentNodes
    {
        get { return adjacentNodes; }
        set { adjacentNodes = value; }
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void ChangeColor(bool onPath)
    {
        if (!walkable)
        {
            return;
        }
        else
        {
            if (onPath)
                renderer.material.color = Color.green;
            else
                renderer.material.color = Color.red;
        }
    }

    public void Reset(bool keepMap)
    {
        if (keepMap)
        {
            InputManager.Instance.ChangeNodeType(this, type);
        }
        else
        {
            InputManager.Instance.ChangeNodeType(this, NodeType.empty);
        }
    }

    public void ChangeColorByType()
    {
        Color c = Color.white;
        switch (type)
        {
            case NodeType.empty:
                {
                    break;
                }
            case NodeType.goal:
                {
                    c = Color.yellow;
                    break;
                }
            case NodeType.slow:
                {
                    c = Color.cyan;
                    break;
                }
            case NodeType.start:
                {
                    c = Color.blue;
                    break;
                }
            case NodeType.wall:
                {
                    c = Color.gray;
                    break;
                }
            default:
                {
                    break;
                }
        }

        renderer.material.color = c;
    }
}
