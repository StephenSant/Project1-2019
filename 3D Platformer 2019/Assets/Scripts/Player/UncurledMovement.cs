using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncurledMovement : MonoBehaviour
{
    public float moveSpeed=5;
    public float jumpHeight=3;

    public bool grounded;

    private Rigidbody rigidbody;
    private Transform parent;

    // Use this for initialization
    void Start()
    {
       //Remember to get everything from the parent
       rigidbody = GameObject.FindWithTag("Player").GetComponentInParent<Rigidbody>();
       parent = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        parent.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        parent.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
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
    }
}
