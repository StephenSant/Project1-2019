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

    // moveDirection is the neutral analogue input position (inputs handled in PlayerInput.cs).
    private Vector3 moveDirection = Vector3.zero;

    // Components.
    private Rigidbody rigidbody;
    private Transform parent;
    #endregion

    #region Functions 'n' Methods
    
    // Where we get our components.
    #region void Start()
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        //Remember to get everything from the parent
        rigidbody = GameObject.FindWithTag("Player").GetComponentInParent<Rigidbody>();
        parent = GameObject.FindWithTag("Player").transform;
    }
    #endregion
    // Where we make a Raycast and check if we're touching the ground.
    #region void Update()
    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        /// Removed (old Move).
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
        /// Removed (old Jump).
        /// if (grounded && Input.GetButton("Jump"))
        /// {
        ///     rigidbody.velocity = ((Vector3.up * 100) * Time.deltaTime * jumpHeight);
        /// }
    } 
    #endregion

    // Where we handle Uncurled Movement.
    #region Armadillo's New Moves
    // Where we Move in all four directions in a single line using moveDirection.
    public void UpdateUncurlMove()
    {
        parent.Translate(moveDirection * Time.deltaTime);
    }

    // Where our inputs from PlayerInput.cs go in and do all the 'maths' for moveDirection.
    public void UncurlMove(float inputH, float inputV)
    {
        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = parent.transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
    }
    // Where we jump. Umm... y-yeah.
    public void UncurlJump()
    {
        if (grounded)
        {
            rigidbody.velocity = ((Vector3.up * 100) * Time.deltaTime * jumpHeight);
        }
    }
    #endregion
    #endregion
}
