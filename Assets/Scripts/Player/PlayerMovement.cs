using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Add Variable
    //存取修飾子public private protected
    public float speed = 6f;
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
        Animating(h, v);
    }
    //Method 
    private void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        //如果有打到地板
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //打到地板的點座標
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            //(x,y,z,w)
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0;
        anim.SetBool("IsWalking", walking);
    }
}
