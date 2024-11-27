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
            // Try to find the child GameObject named "Waypoint"
            Transform playerWaypointTransform = Player.transform.Find("Waypoint");

            if (playerWaypointTransform != null)
            {
                Waypoint playerWaypoint = playerWaypointTransform.GetComponent<Waypoint>();  // Assuming the child has a Waypoint component
                if (playerWaypoint != null)
                {
                    return playerWaypoint.gameObject.GetInstanceID();
                }
                Debug.LogWarning("Waypoint component not found on player's child object!");
            }
            else
            {
                Debug.LogWarning("Player's waypoint child object not found!");
            }
        }
        else
        {
            Debug.LogWarning("Player object is null!");
        }

        return -1;  // Return -1 if no waypoint is found
    }

}
