using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurledMovement : MonoBehaviour
{
    #region Variables
    // Self-explanatory.
    public float moveSpeed = 15;

    // moveDirection is the neutral analogue input position (inputs handled in PlayerInput.cs).
    private Vector3 moveDirection = Vector3.zero;

    // Transform grabbed from (parent) Player object.
    private Transform parent;
    #endregion

    #region Functions 'n' Methods

    // Where we get our Transform (parent) component.
    #region void Start()
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        //Remember to get everything from the parent
        parent = GameObject.FindWithTag("Player").transform;
    } 
    #endregion

    /// CONDEMNED - Update() removed (see '**Explanation**:').
    /// 
    /// **Explanation**: While you can change the keys to Input.GetAxis with 'Edit > Project Settings > Input',
    ///                  it's impossible to change the keys at runtime.
    ///                  Believe me: I checked, and, if I could, I totally would leave this alone.
    /// 
    /// // Update is called once per frame, if MonoBehaviour is enabled
    /// void Update()
    /// {
    ///     parent.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
    ///     parent.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
    /// }

    // Where we handle Curled Movement
    #region Armadillo's New Moves
    // Where we Move in all four directions in a single line using moveDirection.
    public void UpdateCurlMove()
    {
        parent.Translate(moveDirection * Time.deltaTime);
    }

    // Where our inputs from PlayerInput.cs go in and do all the 'maths' for moveDirection.
    public void CurlMove(float inputH, float inputV)
    {
        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = parent.transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;
    }
    #endregion
    #endregion
}
