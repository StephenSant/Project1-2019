using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerInput makes it possible to use rebound keys.
public class PlayerInputMkII : MonoBehaviour
{
    // Nothing but references to scripts which give/use keys.
    #region Variables
    // Format is: public [SCRIPT NAME HERE] var;
    public MenuHandler key;
    public PlayerControllerMkII playerControl;
    public Pickup flipControl;

    // Input Axis (Forward, Backward, Left, Right).
    float inputH = 0;
    float inputV = 0;

    // Control dampening (how quickly it starts and stops).
    public float dampen;
    #endregion

    #region Functions 'n' Methods
    // Where we get our control scripts.
    #region void Start()
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        // Get our MenuHandler's saved keybindings, and...
        key = FindObjectOfType<MenuHandler>();

        // ... the relevant functions within the scripts.
        playerControl = FindObjectOfType<PlayerControllerMkII>();
        flipControl = FindObjectOfType<Pickup>();
    }
    #endregion

    // Where we check our key inputs and execute our functions.
    #region void Update()
    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        /// ATTENTION! CHECK COMPATIBILITY WITH MOVEMENT SCRIPTS!
        #region Movement  

        // Left and Right axis.
        #region inputH dampening
        // If we press... uh, yeah. It turns on.
        if (Input.GetKey(key.right))
        {
            inputH = 1;
        }
        if (Input.GetKey(key.left))
        {
            inputH = -1;
        }
        // Otherwise, if we're not pressing the keys...
        else if (!Input.GetKey(key.right) && !Input.GetKey(key.left))
        {
            // Start slowing the player down smoothly to a stop.
            if (inputH > 0.05f && inputH != 0)
            {
                inputH -= Time.deltaTime / dampen;
            }
            else if (inputH < -0.05f && inputH != 0)
            {
                inputH += Time.deltaTime / dampen;
            }
            else if (inputH <= 0.05f && inputH >= -0.05f)
            {
                inputH = 0;
            }
        }
        #endregion
        // Forward and Backward axis.
        #region inputV dampening
        // See 'inputH dampening' for code comments.
        if (Input.GetKey(key.forward))
        {
            inputV = 1;
        }
        if (Input.GetKey(key.backward))
        {
            inputV = -1;
        }

        else if (!Input.GetKey(key.forward) && !Input.GetKey(key.backward))
        {
            if (inputV > 0.05f && inputV != 0)
            {
                inputV -= Time.deltaTime / dampen;
            }
            else if (inputV < -0.05f && inputV != 0)
            {
                inputV += Time.deltaTime / dampen;

            }
            else if (inputV <= 0.05f && inputV >= -0.05f)
            {
                inputV = 0;
            }
        }
        #endregion

        // Plug our inputs into our Controller scripts from here.
        playerControl.Move(inputH, inputV);

        // Same as above, but for 'UncurledMovement.Jump()'.
        if (Input.GetKey(key.jump) && playerControl.isCurled == false)
        {
            playerControl.Jump();
        }

        if (!Pause.paused)
        {
            if (Input.GetKey(key.curl) && playerControl.curlLock == false)
            {
                playerControl.Curl();
            }
            else if (Input.GetKeyUp(key.curl) && playerControl.curlLock == false)
            {
                playerControl.isCurled = false;
                playerControl.curCurlTime = 0f;
            }
        }

        playerControl.UpdateMove();
        #endregion
    }
    #endregion 
    #endregion
}
