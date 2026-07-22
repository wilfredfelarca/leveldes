using UnityEngine;
public class Follower : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float followDistance = 3f; // social distancing the code!
    [SerializeField] Transform player;
    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing && player != null)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // little guy will only follow if the player is further than our follow distance.
        // dw, he can wait.
        if (distance > followDistance)
        {
            Vector3 previousPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            
            Vector3 direction = transform.position - previousPosition;
            direction.y = 0f; // just to make sure our guys dont tilt up or down.
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryStartFollowing(collision.gameObject);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        TryStartFollowing(collision.gameObject);
    }

    private void TryStartFollowing(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}