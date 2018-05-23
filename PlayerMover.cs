using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        // Creates a mask for the floor
        floorMask = LayerMask.GetMask ("Floor");

        // Set up references
        anim = GetComponent<Animator> ();
        playerRigidBody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw ("Horizontal");
        float v = Input.GetAxisRaw ("Vertical");

        Move (h, v);
        Turning();
        Animating (h, v);
    }

    void Move (float h, float v)
    {
        movement.Set(h, 0f, v);

        // movement = direction * speed * time(int this case, per second)
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition (transform.position + movement);
    }

    void Turning()
    {
        // Create a ray from the camera lens to the mouse cursor
        Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

        // Store the raycast info
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
            playerRigidBody.MoveRotation (newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool ("IsWalking", walking);
    }
}
