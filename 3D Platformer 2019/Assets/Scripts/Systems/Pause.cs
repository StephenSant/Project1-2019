using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    #region Variables
    public static bool paused; // Needs to be static.

    // Make our pause menu stuff.
    public GameObject pauseMenu;
    public bool resumeButton;

    // Other component tidbits.
    public MenuHandler handler;
    #endregion

    // Where we set our default time state on initialization.
    #region void Start() - Set Time state
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        // The game is running, and our pauseMenu is off at start.
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    #endregion

    // Where we check to actually execute pausing.
    #region void Update() - Execute TogglePause()
    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        // If we press the Escape key... execute TogglePause().
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    #endregion

    // Where we define what pausing does.
    #region +void TogglePause - Open/Close Pause Window
    public void TogglePause()
    {
        // If the game is paused, and we're NOT in the Options Menu... resume (un-pause) the game.
        if (paused && !handler.showOptions)
        {
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Or If the game is paused and we ARE in the Options Menu... execute MenuHandler's ToggleOptions() (go back to Pause Menu).
        else if (paused && handler.showOptions)
        {
            handler.ToggleOptions();
            pauseMenu.SetActive(true);
        }

        // Otherwise (if the game is NOT paused)... pause the game.
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Toggle the Pause Menu on and off (show and hide the canvas Pause Menu).
        pauseMenu.SetActive(paused);
    }
    #endregion
}
