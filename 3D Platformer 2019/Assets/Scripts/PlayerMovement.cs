using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public bool grounded;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
       rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (grounded && Input.GetButton("Jump"))
        {
            rigidbody.velocity = ((Vector3.up*100) * Time.deltaTime * jumpHeight);
        }
        if (transform.position.y <= -5)
        {
            transform.position = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
