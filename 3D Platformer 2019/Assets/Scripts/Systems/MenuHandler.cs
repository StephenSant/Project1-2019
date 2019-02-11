using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement; // For carrying information over between scene swaps.
using UnityEngine.UI; // For enabling interaction with Unity's canvas GUI elements.
using UnityEngine.EventSystems; // For enabling interaction with Unity's UI EventSystem (in all its glory...!~).

public class MenuHandler : MonoBehaviour
{
    #region Variables
    [Header("Options")]
    // Toggle options menu on and off.
    public bool showOptions;

    // New toggle made for the 'tab'-based options menu design.
    public bool showKeybinds;
    
    // Make an array of all compatible screen resolutions for a 16:9 aspect ratio.
    // Current value of chosen resolution setting (pulled from resDropDown).
    // "Is the game displayed in fullscreen?" (required when setting resolution (Screen.SetResolution)).
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullscreen;

    // Individual field for every control input KeyCode (required to save/load keybindings).
    [Header("Keys")]
    public KeyCode holdingKey;
    public KeyCode forward, backward, left, right, jump, curl;
    // private holdKey is if we change our keys, but we don't save.
    private KeyCode holdKeyForward, holdKeyBackward, holdKeyLeft, holdKeyRight, holdKeyJump, holdKeyCurl;

    // Hold Values needed to fall back on if you change video settings, but you click 'cancel'.
    [Header("Hold Values")]
    public float holdVol;
    public float holdBright, holdAmbLight;

    [Header("References")]
    // Reference to your main AudioSource.
    // Reference your scene's directional Light.
    // Reference resolution Dropdown menu (this is where resIndex gets its value from).
    // Reference sliders (options menu).
    public AudioSource mainAudio;
    public Light dirLight;
    public Dropdown resDropDown;
    public Slider volSlider, brightSlider, ambLightSlider;

    // Grab the Main Menu and Options Menu from the scene.
    public GameObject mainMenu, optionsMenu;

    // Grab these options menu tabs for toggling
    public GameObject optionsGeneral, optionsKeybinds;

    [Header("KeyBind References")]
    // Make a Text placeholder for every control input (control input stuff).
    public Text forwardText;
    public Text backwardText, leftText, rightText, jumpText, curlText;
    #endregion
    
    #region Functions 'n' Methods

    #region Start
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        // Get relevant component(s) to 'mainAudio' and 'dirLight' (allows internal values to be linked to sliders later).
        mainAudio = GameObject.Find("MainMusic").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        // Try to load in KeyCode(s) stored in the System's save file, otherwise fallback to PlayerPrefs for default keys.
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        curl = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Curl", "Mouse0"));

        // Make the currently assigned KeyCode display on the keybind buttons (otherwise it'll be blank on start).
        forwardText.text = forward.ToString();
        backwardText.text = backward.ToString();
        leftText.text = left.ToString();
        rightText.text = right.ToString();
        jumpText.text = jump.ToString();
        curlText.text = curl.ToString();

        //(?) Loading other saved settings.
        mainAudio.volume = PlayerPrefs.GetFloat("Volume", mainAudio.volume);
        dirLight.intensity = PlayerPrefs.GetFloat("Light", dirLight.intensity);
        RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("Ambient", RenderSettings.ambientIntensity);
    }
    #endregion

    #region Main Menu Stuff
    // Each of these functions are connected to the relevant canvas buttons to make the canvas UI actually functional.
    #region Load Game
    // Method to load game scene.
    public void PlayGame()
    {
        // Mhmm.
        SceneManager.LoadScene(1);
    }
    #endregion
    /// Garbage.
    /// // This used to be a 'continue' button, but found it to be out of place from the final intent.
    /// #region Load Game
    /// // Method to load game scene.
    /// public void LoadGame()
    /// {
    ///     // See?
    ///     SceneManager.LoadScene(2);
    /// }
    /// #endregion
    #region Quit Game
    // Method to exit game (return to main menu).
    public void QuitGame()
    {
        // I mean, it works.
        SceneManager.LoadScene(0);
    }
    #endregion
    #region Exit Execultable
    // Method to exit game ('Alt + F4' is what... turkey's... do?).
    public void ExitExe()
    {
        // Quit Application/Editor's play mode.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion
    #region Toggle Options
    // Method to toggle options menu (acts upon the bool below).
    public void ToggleOptions()
    {
        OptionToggle();
    }
    #endregion
    #region Options Toggle
    // bool to return conditions on when to display or hide option's menu (and do a few other things).
    bool OptionToggle()
    {
        // If we click 'cancel' (if 'showOptions' is true)... close the options menu and return to the main menu.
        if (showOptions)
        {
            showOptions = false;
            showKeybinds = false;
            // Oh, and set the options menu back to the General tab (to prevent a null reference in the else state).
            optionsGeneral.SetActive(true);
            optionsKeybinds.SetActive(false);
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);

            // Reset all of our options back to our previous settings.
            #region Cancel Saving - Reset all settings
            mainAudio.volume = holdVol;
            dirLight.intensity = holdBright;
            ambLightSlider.value = holdAmbLight;

            forward = holdKeyForward;
            backward = holdKeyBackward;
            left = holdKeyLeft;
            right = holdKeyRight;
            jump = holdKeyJump;
            curl = holdKeyCurl;

            forwardText.text = holdKeyForward.ToString();
            backwardText.text = holdKeyBackward.ToString();
            leftText.text = holdKeyLeft.ToString();
            rightText.text = holdKeyRight.ToString();
            jumpText.text = holdKeyJump.ToString();
            curlText.text = holdKeyCurl.ToString(); 
            #endregion

            return true;
        }
        // otherwise (false)... open the options menu and hide the main menu (and do those other things we talked about).
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);

            /// Now for those other things:
            /// Upon opening the options menu...
            /// Get each relevant canvas GUI component from their respective GameObject.
            /// Set each slider position to their respective control element's value (prevents spooky spikes upon adjustment).
            /// Get a placeholder value for sliders.
            volSlider = GameObject.Find("Slider (Volume)").GetComponent<Slider>();
            volSlider.value = mainAudio.volume;
            holdVol = volSlider.value;
            
            brightSlider = GameObject.Find("Slider (Brightness)").GetComponent<Slider>();
            brightSlider.value = dirLight.intensity;
            holdBright = brightSlider.value;
            
            ambLightSlider = GameObject.Find("Slider (Ambient Light)").GetComponent<Slider>();
            ambLightSlider.value = RenderSettings.ambientIntensity;
            holdAmbLight = ambLightSlider.value;

            resDropDown = GameObject.Find("Dropdown (Resolution)").GetComponent<Dropdown>();

            return false;
        }
    }
    #endregion
    #region Toggle Keybinds
    // Method to toggle Keybinds tab (acts upon the bool set below).
    public void ToggleKeybinds()
    {
        KeybindToggle();
    }
    #endregion
    #region Keybinds Toggle
    // A bool that's almost identical to 'OptionToggle()' (toggles between the 'General' tab and the 'Keybinds' tab.
    bool KeybindToggle()
    {
        // Close Keybinds tab, open General tab.
        if (showKeybinds)
        {
            showKeybinds = false;
            optionsGeneral.SetActive(true);
            optionsKeybinds.SetActive(false);
            return true;
        }
        // Close General tab, open Keybinds tab.
        else
        {
            showKeybinds = true;
            optionsGeneral.SetActive(false);
            optionsKeybinds.SetActive(true);
            // Store current keybinds on opening Keybinds (if we do or don't save).
            holdKeyForward = forward;
            holdKeyBackward = backward;
            holdKeyLeft = left;
            holdKeyRight = right;
            holdKeyJump = jump;
            holdKeyCurl = curl;
            return false;
        }
    } 
    #endregion

    #endregion

    #region Options Menu Stuff
    // All of these Methods are assigned to their respective Unity canvas elements and are... hmm... déjà vu...
    #region +void Volume() - Volume Slider
    // Method to control mainAudio volume via the options menu volSlider.
    public void Volume()
    {
        mainAudio.volume = volSlider.value;
    }
    #endregion
    #region +void Brightness() - Directional Light Slider
    // Method to control dirLight brightness (intesity) via the brightSlider.
    public void Brightness()
    {
        dirLight.intensity = brightSlider.value;
    }
    #endregion
    #region +void Ambient() - Ambient Light Slider
    // Method to control ambient light intensity (ambientIntensity) via the ambLightSlider.
    public void Ambient()
    {
        RenderSettings.ambientIntensity = ambLightSlider.value;
    }
    #endregion
    #region +void Resolution() - Resolution Dropdown
     // Method to change the screen resolution via the resDropDown.
    public void Resolution()
    {
        resIndex = resDropDown.value;
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullscreen);
    }
    #endregion
    #region +void Save() - Save Button
    // Method to save all KeyCodes to strings in the PlayerPrefs.
    public void Save()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Curl", curl.ToString());

        // Saving other changed settings
        PlayerPrefs.SetFloat("Volume", mainAudio.volume);
        PlayerPrefs.SetFloat("Light", dirLight.intensity);
        PlayerPrefs.SetFloat("Ambient", RenderSettings.ambientIntensity);

        // Set our hold values to our new values.
        holdVol = volSlider.value;
        holdBright = brightSlider.value;
        holdAmbLight = ambLightSlider.value;
        holdKeyForward = forward;
        holdKeyBackward = backward;
        holdKeyLeft = left;
        holdKeyRight = right;
        holdKeyJump = jump;
        holdKeyCurl = curl;

        OptionToggle();
    }
    #endregion
    #endregion
    
    // Where we interact with the GUI to setup our KeyCodes
    #region -void OnGUI() - KeyCode Setup
    // OnGUI is called for rendering and handling GUI events
    private void OnGUI()
    {
        #region Keycodes
        // Get the current Event being processed right now.
        Event e = Event.current;

        #region Forward
        // "if 'forward' has NO KeyCode BOUND..."
        if (forward == KeyCode.None)
        {
            // "... Log a warning in the console until a new keyCode is received."
            Debug.Log("KeyCode: " + e.keyCode);

            // "if the current Event keyCode is NOT empty..." (or: "if I input a new KeyCode as the current Event...")
            if (e.keyCode != KeyCode.None)
            {
                // "... if the current keyCode Event is NOT already in use by any of these other controls..."
                if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == curl))
                {
                    // "... set 'forward' to its new keyCode, empty holdingKey, and display the new key as a string."
                    forward = e.keyCode;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
                // "else..." (or: "if the new KeyCode you're trying to set IS already in use by another control...")
                else
                {
                    // "... do nothing/keep waiting until a free/available KeyCode is pressed." (Prevents double-binds.)
                    forward = holdingKey;
                    holdingKey = KeyCode.None;
                    forwardText.text = forward.ToString();
                }
            }
        }
        #endregion
        #region Backward
        // No; I am not going to repeat all of that. You're welcome.
        if (backward == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);

            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == curl))
                {
                    backward = e.keyCode;
                    holdingKey = KeyCode.None;
                    backwardText.text = backward.ToString();
                }
                else
                {
                    backward = holdingKey;
                    holdingKey = KeyCode.None;
                    backwardText.text = backward.ToString();
                }
            }
        }
        #endregion
        #region Left
        if (left == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);

            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == curl))
                {
                    left = e.keyCode;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
                else
                {
                    left = holdingKey;
                    holdingKey = KeyCode.None;
                    leftText.text = left.ToString();
                }
            }
        }
        #endregion
        #region Right
        if (right == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);

            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == curl))
                {
                    right = e.keyCode;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
                else
                {
                    right = holdingKey;
                    holdingKey = KeyCode.None;
                    rightText.text = right.ToString();
                }
            }
        }
        #endregion
        #region Jump
        if (jump == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);

            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == curl))
                {
                    jump = e.keyCode;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
                else
                {
                    jump = holdingKey;
                    holdingKey = KeyCode.None;
                    jumpText.text = jump.ToString();
                }
            }
        }
        #endregion
        #region Curl
        if (curl == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);

            if (e.keyCode != KeyCode.None)
            {
                if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump))
                {
                    curl = e.keyCode;
                    holdingKey = KeyCode.None;
                    curlText.text = curl.ToString();
                }
                else
                {
                    curl = holdingKey;
                    holdingKey = KeyCode.None;
                    curlText.text = curl.ToString();
                }
            }
        }
        #endregion
        #endregion
    }

    // I couldn't think of a name for this. Everything here is what makes the canvas keybind buttons functional.
    #region Canvas-KeyCode Interact-Link
    // Each Key Function uses the naming convention 'Key*Action' to organize it when implementing to Canvas element.
    #region +void KeyAForward()
    public void KeyAForward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || curl == KeyCode.None))
        {
            holdingKey = forward;
            forward = KeyCode.None;
            forwardText.text = forward.ToString();
        }
    }
    #endregion
    #region +void KeyBBackward()
    public void KeyBBackward()
    {
        if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || curl == KeyCode.None))
        {
            holdingKey = backward;
            backward = KeyCode.None;
            backwardText.text = backward.ToString();
        }
    }
    #endregion
    #region +void KeyCLeft()
    public void KeyCLeft()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || curl == KeyCode.None))
        {
            holdingKey = left;
            left = KeyCode.None;
            leftText.text = left.ToString();
        }
    }
    #endregion
    #region +void KeyDRight()
    public void KeyDRight()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || jump == KeyCode.None || curl == KeyCode.None))
        {
            holdingKey = right;
            right = KeyCode.None;
            rightText.text = right.ToString();
        }
    }
    #endregion
    #region +void KeyEJump()
    public void KeyEJump()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || curl == KeyCode.None))
        {
            holdingKey = jump;
            jump = KeyCode.None;
            jumpText.text = jump.ToString();
        }
    }
    #endregion
    #region +void KeyFCurl()
    public void KeyFCurl()
    {
        if (!(forward == KeyCode.None || backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None))
        {
            holdingKey = curl;
            curl = KeyCode.None;
            curlText.text = curl.ToString();
        }
    }
    #endregion
    #endregion

    #endregion

    #endregion
}
