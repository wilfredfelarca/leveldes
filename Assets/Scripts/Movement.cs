using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed;
    //public float sprintSpeed;
    public float rotationSpeed = 20f; // higher = snappier turning

    private Vector3 boxHalf = new Vector3(0.6f, 0.6f, 0.3f);
    LayerMask wallMasks;

    void Awake()
    {
        wallMasks = LayerMask.GetMask("Wall");
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetAxisRaw("Horizontal") == 1)
            CheckWallThenMove(Vector3.left);

        else if (Input.GetAxisRaw("Horizontal") == -1)
            CheckWallThenMove(Vector3.right);

        if (Input.GetAxisRaw("Vertical") == 1)
            CheckWallThenMove(Vector3.back);

        else if (Input.GetAxisRaw("Vertical") == -1)
            CheckWallThenMove(Vector3.forward);

        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        //Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //float speed = moveSpeed; //Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        //// Apply movement with wall collision check
        //if (moveDirection.sqrMagnitude > 0.0001f)
        //{
        //    if (!CheckWallCollision(moveDirection))
        //    {
        //        transform.position += moveDirection * -speed * Time.deltaTime;
        //    }

        //    Quaternion targetRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //}
    }

    void CheckWallThenMove(Vector3 moveDir)
    {
        //Wall collision is genuinely miserable to figure out
        //this went through like nine iterations, this one being the simplest
        RaycastHit _hit;
        if (!Physics.BoxCast(
            transform.position,  //Origin of boxcast, center of object
            boxHalf,             //Half of the size of the box, seriously who designed this
            moveDir,             //Direction of boxcast, the direction we want to move in
            out _hit,             //Variable to store hit information
            transform.rotation,  //Orientation of boxcast, same as object rotation
            0.2f,                //Distance of boxcast, ahead of the object to predict collisions and prevent clipping
            wallMasks))          //Layers to collide with
        { transform.position += moveDir * moveSpeed * Time.deltaTime; }
    }

    //private bool CheckWallCollision(Vector3 moveDir)
    //{
    //    RaycastHit hit;
    //    if (Physics.BoxCast(
    //        transform.position,      // Origin of boxcast, center of object
    //        boxHalf,                 // Half of the size of the box
    //        moveDir,                 // Direction of boxcast
    //        out hit,                 // Variable to store hit information
    //        transform.rotation,      // Orientation of boxcast
    //        0.6f))                   // Distance of boxcast, ahead of the object to predict collisions
    //    {
    //        // Check if hit object has "Wall" tag
    //        if (hit.collider.gameObject.CompareTag("Wall"))
    //            return true;
    //    }
    //    return false;
    //}

    void OnDrawGizmos()
    {
        //Collision Wireframe
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxHalf * 2f + new Vector3(0.3f, 0f, 0.3f));
        //We're adding 0.3f to WireCube because boxcast is bigger than the actual player
    }
}