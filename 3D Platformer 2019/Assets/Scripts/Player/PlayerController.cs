using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        curled = GameObject.Find("Curled");
        uncurled = GameObject.Find("Uncurled");
        isCurled = false;
        rigidbody = GetComponent<Rigidbody>();
        cooldownSlider.maxValue = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        #region Curling
        if (Input.GetMouseButton(0) && ballTimer>=0 && cooldownTimer == 0)
        {
            isCurled = true;
            ballTimer -= Time.deltaTime;
        }
        else
        {
            isCurled = false;
            ballTimer = ballTime;
        }
        #endregion
        #region Uncurling
        if ((Input.GetMouseButtonUp(0) || ballTimer <= 0) && cooldownTimer == 0)
        {
            cooldownTimer = cooldown;
        }
        #endregion

        cooldownTimer -= Time.deltaTime; //Cooldown counts down

        if (cooldownTimer < 0) cooldownTimer = 0; //If cooldown is done.

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
}
