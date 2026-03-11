using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PropMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rollSpeed = 300f;

    public GameObject propObject;

    Rigidbody rb;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Shift’·‰Ÿ‚µ’†
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ResetPropRotation();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Vector3 velocity = move * moveSpeed;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        if (propObject != null && move.magnitude > 0.1f)
        {
            Vector3 axis = new Vector3(move.z, 0, -move.x);
            propObject.transform.Rotate(axis * rollSpeed * Time.deltaTime);
        }
    }
    void ResetPropRotation()
    {
        if (propObject != null)
        {
            propObject.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}