using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncurledMovement : MonoBehaviour
{
    #region Variables
    // Self-explanatory.
    public float moveSpeed = 5;
    public float jumpHeight = 3;
    // Are we touching the ground?
    public bool grounded;

    // moveDirection is the neutral analogue input position (handled in PlayerInput.cs).
    private Vector3 moveDirection = Vector3.zero;

    // Components.
    private Rigidbody rigidbody;
    private Transform parent;
    #endregion

    #region Functions 'n' Methods

    // Where we get components.
    #region Start()
    // Use this for initialization
    void Start()
    {
        //Remember to get everything from the parent
        rigidbody = GameObject.FindWithTag("Player").GetComponentInParent<Rigidbody>();
        parent = GameObject.FindWithTag("Player").transform;
    }
    #endregion
    // Where we make and check our Raycast.
    #region Update()
    // Update is called once per frame
    void Update()
    {
        /// parent.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        /// parent.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        ///if (grounded && Input.GetButton("Jump"))
        ///{
        ///    rigidbody.velocity = ((Vector3.up * 100) * Time.deltaTime * jumpHeight);
        ///}
    }
    #endregion
                          // ↓ ↓ ↓ ↓ ↓ This is important if you want to get rebindable keys working.
    // Where our inputs from PlayerInput.cs make us do things.
    #region Movement Stuff
    
    public void UpdateUncurlMove()
    {
        parent.Translate(moveDirection * Time.deltaTime);
    }
    
    // Where our axis inputs go in and make us move.
    #region +UncurlMove(float inputH, float inputV)
    public void UncurlMove(float inputH, float inputV)
    {
        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = parent.transform.TransformDirection(moveDirection); // ← Will deltaTime break it?
        moveDirection *= moveSpeed;
    }
    #endregion
    // Where we jump. Umm... y-yeah.
    #region +UncurlJump()
    public void UncurlJump()
    {
        if (grounded)
        {
            rigidbody.velocity = ((Vector3.up * 100) * Time.deltaTime * jumpHeight);
        }
    } 
    #endregion
    #endregion
    #endregion
}
