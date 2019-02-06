using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurledMovement : MonoBehaviour
{
    #region Variables
    // Self-explanatory.
    public float moveSpeed = 15;

    // moveDirection is the neutral analogue input position (handled in PlayerInput.cs).
    private Vector3 moveDirection = Vector3.zero;

    private Transform parent;
    #endregion

    #region Functions 'n' Methods

    // Where we get components.
    #region Start()
    // Use this for initialization
    void Start()
    {
        //Remember to get everything from the parent
        parent = GameObject.FindWithTag("Player").transform;
    }
    #endregion

    #region /// Update()
    /// // Update is called once per frame
    /// void Update()
    /// {
    ///     parent.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
    ///     parent.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
    /// } 
    #endregion

    public void UpdateCurlMove()
    {
        parent.Translate(moveDirection * Time.deltaTime);
    }

    // Where our axis inputs go in and make us move.
    #region +CurlMove(float inputH, float inputV)
    public void CurlMove(float inputH, float inputV)
    {
        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = parent.transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
    } 
    #endregion
    #endregion
}
