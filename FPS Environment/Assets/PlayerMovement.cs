
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    private Rigidbody rb;
    public float jumpForce = 10f;

    private float fwdInput;
    private float horizontalInput;
    private bool isGrounded = true;

    private float gravity = 9.81f;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        ///Move();

        //player movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        fwdInput = Input.GetAxisRaw("Vertical");
        rb.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime * fwdInput, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.right * moveSpeed * Time.deltaTime * horizontalInput, ForceMode.Impulse);
        if (!isGrounded)
        {
            moveSpeed = 5f;
            rb.drag = 0f;
        }
        else
        {
            rb.drag = 5f;
            moveSpeed = 50f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = 100f;
        }

        //player jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2f * jumpForce * gravity), rb.velocity.z);
            isGrounded = false;
        }

        // Apply gravity to simulate a more realistic jump
        rb.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime;

        // Limit the upward velocity to make the jump less floaty
        if (rb.velocity.y > 0f && rb.velocity.y > jumpForce)
        {
            rb.velocity = new Vector3(rb.velocity.x,  jumpForce, rb.velocity.z);
        }
        
    }

    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag ("Ground")){
            isGrounded = true;
        }
    }
}
