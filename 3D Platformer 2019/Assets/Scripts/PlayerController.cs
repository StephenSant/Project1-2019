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

    public float cooldown;
    private float cooldownTimer;
    public bool canCurl;
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

    /// // Update is called once per frame
    /// void Update()
    /// {
    ///     Debug.Log(ballTimer);
    ///     if (Input.GetMouseButton(0) && ballTimer>=0 && cooldownTimer == 0)
    ///     {
    ///         isCurled = true;
    ///         ballTimer -= Time.deltaTime;
    ///     }
    ///     else
    ///     {
    ///         isCurled = false;
    ///         ballTimer = ballTime;
    ///     }
    ///     if ((Input.GetMouseButtonUp(0) || ballTimer <= 0) && cooldownTimer == 0)
    ///     {
    ///         cooldownTimer = cooldown;
    ///     }
    ///     cooldownTimer -= Time.deltaTime;
    ///     if (cooldownTimer < 0) cooldownTimer = 0;
    ///     switch (isCurled)
    ///     {
    ///         case false:
    ///             uncurled.SetActive(true);
    ///             curled.SetActive(false);
    ///             break;
    ///         case true:
    ///             uncurled.SetActive(false);
    ///             curled.SetActive(true);
    ///             break;
    /// 
    ///     }
    ///     if (transform.position.y <= -5)
    ///     {
    ///         transform.position = Vector3.zero;
    ///         rigidbody.velocity = Vector3.zero;
    ///     }
    /// 
    ///     cooldownSlider.value = cooldownTimer;
    /// 
    /// }

    public void Curl()
    {
        Debug.Log(ballTimer);
        if (ballTimer >= 0 && cooldownTimer == 0)
        {
            isCurled = true;
            ballTimer -= Time.deltaTime;
        }
        else
        {
            isCurled = false;
            ballTimer = ballTime;
        }
        if ((ballTimer <= 0) && cooldownTimer == 0)
        {
            cooldownTimer = cooldown;
        }
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer < 0) cooldownTimer = 0;
        switch (isCurled)
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
        if (transform.position.y <= -5)
        {
            transform.position = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }

        cooldownSlider.value = cooldownTimer;
    }
}
