using UnityEngine;

/// <summary>
/// Deletes any object that collides with this object if it has the specified tag.
/// Has functionality for both collision and trigger events.
/// </summary>

public class DeleteCollided : MonoBehaviour
{
    [SerializeField]
    private string tagToDelete = "Food"; // The tag of the object to delete
    public Transform[] waypoints; // waypoints for the enemy to go
    public float speed = 10f; // speed of enemy
    private int currentWaypoint = 0;

    void Update()
    {
        if (waypoints.Length == 0)
        {
            // prevents array going below zero
            return; 
        }
        
        // move the enemy towards waypoints
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Only destroys the colliding object if it has the specified tag
        if (collision.gameObject.CompareTag(tagToDelete))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagToDelete))
        {
            Destroy(collision.gameObject);
        }
    }

}
