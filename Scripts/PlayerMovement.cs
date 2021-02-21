using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float force, maxSpeed;
    [SerializeField] Animator playerAnims;

    private Vector3 direction;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = transform.right * x + transform.forward * z;

        rb.AddForce(direction * force * Time.deltaTime, ForceMode.Acceleration);

        if (rb.velocity.magnitude > 2f)
            playerAnims.SetBool("Walking", true);
        else
            playerAnims.SetBool("Walking", false);


        if (rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed || rb.velocity.z > maxSpeed || rb.velocity.z < -maxSpeed) 
        {
            Vector3 vel = rb.velocity.normalized * maxSpeed * Time.deltaTime;
            vel.y = rb.velocity.y;
            rb.velocity = vel;
        }
    }
}
