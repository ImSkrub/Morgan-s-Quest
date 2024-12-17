using UnityEngine;

public class GraphManager : MonoBehaviour
{
    private Graph graph;

    private void Start()
    {
        graph = gameObject.AddComponent<Graph>();

        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (var waypoint in waypoints)
        {
            graph.AddVertice(waypoint.gameObject);
        }

        for (int i = 0; i < waypoints.Length; i++)
        {
            for (int j = i + 1; j < waypoints.Length; j++)
            {
                graph.AddArista(waypoints[i].gameObject.GetInstanceID(), waypoints[j].gameObject.GetInstanceID(), 1);
            }
        }
    }

    public Graph GetGraph()
    {
        return graph;
    }
}
