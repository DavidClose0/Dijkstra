using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dijkstra;

public class Pathfinder : Kinematic
{
    public Node start;
    public Node goal;
    public Material goalMaterial;
    Graph myGraph;

    FollowPath myMoveType;
    LookWhereGoing myRotateType;

    GameObject[] myPath;

    private List<Node> allNodes;
    private Material defaultMaterial;
    private float targetThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;

        myMoveType = new FollowPath();
        myMoveType.character = this;

        myGraph = new Graph();
        myGraph.Build();

        allNodes = new List<Node>(GameObject.FindObjectsByType<Node>(FindObjectsSortMode.None));
        defaultMaterial = allNodes[0].GetComponent<Renderer>().material;

        goal = GetRandomNodeExcluding(start);
        goal.gameObject.GetComponent<Renderer>().material = goalMaterial;

        ComputePath();
        myMoveType.path = myPath;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();

        if (Vector3.Distance(transform.position, goal.transform.position) < targetThreshold)
        {
            ChooseNewGoal();
        }
    }

    // Computes the path from the current start to the current goal
    private void ComputePath()
    {
        List<Connection> pathConnections = Dijkstra.pathfind(myGraph, start, goal);
        myPath = new GameObject[pathConnections.Count + 1];
        int i = 0;
        foreach (Connection c in pathConnections)
        {
            myPath[i] = c.GetFromNode().gameObject;
            i++;
        }
        myPath[i] = goal.gameObject;

        myMoveType.path = myPath;
        myMoveType.pathIndex = 0;
        myMoveType.target = myPath[0];
    }

    // Chooses a new random goal: the previous goal becomes the new start,
    // its special material is removed, and a new goal is picked and marked.
    private void ChooseNewGoal()
    {
        goal.gameObject.GetComponent<Renderer>().material = defaultMaterial;
        start = goal;
        goal = GetRandomNodeExcluding(start);
        goal.gameObject.GetComponent<Renderer>().material = goalMaterial;
        ComputePath();
    }

    // Returns a random node from allNodes that is not equal to the provided node
    private Node GetRandomNodeExcluding(Node exclude)
    {
        List<Node> candidates = new List<Node>();
        foreach (Node n in allNodes)
        {
            if (n != exclude)
            {
                candidates.Add(n);
            }
        }

        return candidates[Random.Range(0, candidates.Count)];
    }
}
