using UnityEngine;

public class Movetest : MonoBehaviour
{
    float movespeed = 5f;
    [SerializeField] Animator animator;
    bool isgrounded = false;
    Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       body = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction.Normalize();
        transform.position += direction * movespeed * Time.deltaTime;
        float speed = direction.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space)&&isgrounded)
        {
            Jump();
        }
        animator.SetFloat("FallSpeed", body.linearVelocity.y);
        animator.SetBool("IsGround",isgrounded);
    }

    private void Jump()
    {
       body.AddForce(Vector3.up * 5f,ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isgrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isgrounded =false;
    }
}
