using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public List<Waypoint> Waypoints { get; private set; }
    public GameObject Player { get; private set; }

    private void Awake()
    {
        // Initialize the waypoint list
        Waypoints = new List<Waypoint>();

        // Find all waypoints in the scene
        Waypoints.AddRange(FindObjectsOfType<Waypoint>());

        // Find the player
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogWarning("Player not found in the scene!");
        }
    }

    public int GetPlayerWaypointId()
    {
        if (Player != null)
        {
            Transform playerWaypoint = Player.transform.Find("Waypoint");
            if (playerWaypoint != null)
            {
                return playerWaypoint.gameObject.GetInstanceID();
            }
            Debug.LogWarning("Player's waypoint not found!");
        }
        return -1;
    }

}
