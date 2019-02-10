using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public Rigidbody rigidbody;

    public GameObject curled;
    public GameObject uncurled;

    public bool isCurled;

    [Tooltip("Time until your allowed ball form.")]
    public float cooldown;
    private float cooldownTimer;
    public bool canCurl;
    [Tooltip("Time allowed in ball form.")]
    public float ballTime;
    private float ballTimer;

    public Slider cooldownSlider;
    #endregion

    #region Functions 'n' Methods
    // Where we get our components.
    #region void Start()
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        curled = GameObject.Find("Curled");
        uncurled = GameObject.Find("Uncurled");
        isCurled = false;
        rigidbody = GetComponent<Rigidbody>();
        cooldownSlider.maxValue = cooldown;
    }
    #endregion
    // Where we... umm...?
    #region void Update()
    // Update is called once per frame, if MonoBehaviour is enabled
    void Update()
    {
        #region Curling
        if (ballTimer >= 0 && cooldownTimer == 0)
        {
            isCurled = true;
            canCurl = false;
            ballTimer -= Time.deltaTime;
        }
        else
        {
            isCurled = false;
            canCurl = true;
            ballTimer = ballTime;
        }
        #endregion
        #region Uncurling
        if ((isCurled = false || ballTimer <= 0) && cooldownTimer == 0)
        {
            canCurl = false;
            cooldownTimer = cooldown;
        }
        #endregion

        /// cooldownTimer -= Time.deltaTime; //Cooldown counts down

        if (cooldownTimer < 0)
        {
            canCurl = true;
            cooldownTimer = 0; //If cooldown is done.
        }

        switch (isCurled)//Activates/deactivates GameObjects
        {
            case false:
                uncurled.SetActive(true);
                curled.SetActive(false);
                break;
            case true:
                uncurled.SetActive(false);
                curled.SetActive(true);
                break;

        }

        #region Respawning
        if (transform.position.y <= -5)
        {
            transform.position = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }
        #endregion

        cooldownSlider.value = cooldownTimer; //Set slider to cooldown

    }
    #endregion
    // Where we handle our curl toggling (all the timer stuff is still handled in 'Update()').
    #region +void Curl()
    public void Curl()
    {
        if (canCurl == true)
        {
            isCurled = true;
            cooldownTimer -= Time.deltaTime; //Cooldown counts down
        }
        else
        {
            isCurled = false;
        }
    }
    #endregion
    #endregion
}
