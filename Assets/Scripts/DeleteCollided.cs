using UnityEngine;

/// <summary>
/// Deletes any object that collides with this object if it has the specified tag.
/// Has functionality for both collision and trigger events.
/// </summary>

public class DeleteCollided : MonoBehaviour
{
    [SerializeField]
    private string tagToDelete = "Food, Buddy"; // The tag of the object to delete
    public Transform[] waypoints; // waypoints for the enemy to go
    public float speed = 10f; // speed of enemy
    private int currentWaypoint = 0;

    [SerializeField]
    private Sugar sugar; 
    private Antfriends antfriends;

    bool playerJustHit = false;
    float invisTimeLength = 3f; //Seconds of invisibility after being hit
    float invisTimeRemaining = 0f; //Seconds of invisibility after being hit

    void Update()
    {
        // Set a timer for the player to be invincible for a few seconds after being hit
        if (playerJustHit)
        {
            invisTimeRemaining -= Time.deltaTime;
            Debug.Log("Invisibility time remaining: " + invisTimeRemaining);
            if (invisTimeRemaining <= 0f)
            {
                playerJustHit = false;
            }
        }

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
            HandleCollision(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(tagToDelete))
        {
            HandleCollision(collision.gameObject);
        }
    }

    private void HandleCollision(GameObject other)
    {
        if (other.CompareTag("Food"))
        {
            EatFood(other);
        }
        else if (other.CompareTag("Buddy"))
        {
            GetAnt(other);
        }
        else if (other.CompareTag("Player"))
        {
            if (playerJustHit) // If the player was just hit, ignore this collision
                return;
            else
                HitPlayer(other);
        }
    }

    private void EatFood(GameObject food)
    {
        if (sugar != null)
        {
            sugar.sugarAmount++;
        }
        Destroy(food);
    }

    private void HitPlayer(GameObject player)
    {
        if (sugar == null) return;

        if (sugar.sugarAmount <= 0)
        {
            // puts the player in the "Oh shit, Im gonna die" state
            Destroy(player);
        }
        else
        {
            // checks if the sugar is still 1+. if so they the player will not die; they'll lose a sugie though
            sugar.sugarAmount--;
            playerJustHit = true;
            invisTimeRemaining = invisTimeLength; // Reset invisibility time
        }
    }

    private void GetAnt(GameObject buddy)
    {
        if (antfriends != null)
        {
            antfriends.antAmount++;
        }
        
    }

}
