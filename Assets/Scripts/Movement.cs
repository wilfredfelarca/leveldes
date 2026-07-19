using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    public float sprintSpeed;
    public float rotationSpeed = 20f; // higher = snappier turning

    private Vector3 boxHalf = new Vector3(0.3f, 0.6f, 0.3f);

    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Apply movement with wall collision check
        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            if (!CheckWallCollision(moveDirection))
            {
                transform.position += moveDirection * -speed * Time.deltaTime;
            }

            Quaternion targetRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private bool CheckWallCollision(Vector3 moveDir)
    {
        RaycastHit hit;
        if (Physics.BoxCast(
            transform.position,      // Origin of boxcast, center of object
            boxHalf,                 // Half of the size of the box
            moveDir,                 // Direction of boxcast
            out hit,                 // Variable to store hit information
            transform.rotation,      // Orientation of boxcast
            0.2f))                   // Distance of boxcast, ahead of the object to predict collisions
        {
            // Check if hit object has "Wall" tag
            if (hit.collider.gameObject.CompareTag("Wall"))
                return true;
        }
        return false;
    }
    void OnDrawGizmos()
    {
        //Collision Wireframe
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxHalf * 2f + new Vector3(0.3f, 0f, 0.3f));
        //We're adding 0.3f to WireCube because boxcast is bigger than the actual player
    }
}