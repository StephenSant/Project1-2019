﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerInput makes it possible to use rebound keys.
public class PlayerInput : MonoBehaviour
{
    // Nothing but references to scripts which give/use keys.
    #region Variables
    // Format is: public [SCRIPT NAME HERE] var;
    public MenuHandler key;
    public UncurledMovement uncurlController;
    public CurledMovement curlController;
    #endregion

    // Where we get our control scripts.
    #region -void Start()
    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        // Get our MenuHandler's saved keybindings, and...
        key = FindObjectOfType<MenuHandler>();

        // ... the relevant functions within the scripts.
        uncurlController = FindObjectOfType<UncurledMovement>();
        curlController = FindObjectOfType<CurledMovement>();
    }
    #endregion

    // Where we check our key inputs and execute our functions.
    #region void Update()
    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        /// ATTENTION! CHECK COMPATIBILITY WITH MOVEMENT SCRIPTS!
        #region Movement
        // Ternary operator (courtesy of Manny); it's a new (fake) axis using the saved keys.
        float inputH = Input.GetKey(key.right) ? 1f : Input.GetKey(key.left) ? -1f : 0;
        float inputV = Input.GetKey(key.forward) ? 1f : Input.GetKey(key.backward) ? -1f : 0;

        // Execute 'CharacterMovement.Move()' from here.
        uncurlController.Move(inputH, inputV);

        // Same as above, but for 'UncurledMovement.Jump()'.
        if (Input.GetKeyDown(key.jump))
        {
            uncurlController.Jump();
        }

        // controller.UpdateController();
        #endregion
    }
    #endregion
}