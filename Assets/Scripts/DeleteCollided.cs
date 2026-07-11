using UnityEngine;

public class DeleteCollided : MonoBehaviour

{
    [SerializeField]
    private string tagToDelete = "None"; // The tag of the object to delete

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
