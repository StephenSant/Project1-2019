using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;

    public GameObject curled;
    public GameObject uncurled;

    private float maxCurlTime = 1f, curCurlTime = 1f;
    public float curlDrain;
    public float curlRecharge;
    public bool isCurled;
    public bool curlLock;

    // public float cooldown;
    // private float cooldownTimer;
    // public bool canCurl;
    // public float ballTime;
    // private float ballTimer;

    public Slider cooldownSlider;

    // Use this for initialization
    void Start()
    {
        curled = GameObject.Find("Curled");
        uncurled = GameObject.Find("Uncurled");
        isCurled = false;
        curlLock = false;
        curCurlTime = maxCurlTime;
        rigidbody = GetComponent<Rigidbody>();
        cooldownSlider.maxValue = curCurlTime;
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    void Update()
    {
        // If we fall down a pit? Put us back to the start of the level.
        if (transform.position.y <= -5)
        {
            transform.position = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }

        // Clamp curCurlTime recharge to maxCurlTime.
        if (curCurlTime > maxCurlTime)
            curCurlTime = maxCurlTime;

        // If curCurlTime hits zero, stop curling.
        if (curCurlTime < 0)
        {
            curCurlTime = 0;
            isCurled = false;
        }

        // If curCurlTime is less than maxCurlTime AND we stop curling, turn on curlLock and recharge curl.
        if (curCurlTime < maxCurlTime && !isCurled)
        {
            curlLock = true;
            curCurlTime += Time.deltaTime / curlRecharge;
        }

        // If curlLock is on, then we can't carl.
        if (curlLock)   //                  !!! Add to 'curCurlTime < 0'? !!!
            isCurled = false;

        // If our curCurlTime is fully recharged, then end curlLock (we can curl again).
        if (curCurlTime == maxCurlTime)
            curlLock = false;

        // Oooo, switch statement. It changes form.
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

        // Slider shows cooldownTimer.
        cooldownSlider.value = curCurlTime;


        #region Curl Update
        /// // Constantly drain cooldownTimer.
        /// cooldownTimer -= Time.deltaTime;
        /// // Clamp cooldownTimer to zero.
        /// if (cooldownTimer < 0) cooldownTimer = 0;
        /// 
        #endregion
    }

    public void Curl()
    {
        Debug.Log(curCurlTime);
        // Curl into ball and drain ballTimer on-click if ballTimer is zero or higher, and cooldownTimer is zero.
        if (curlLock == false)
        {
            isCurled = true;
            curCurlTime -= Time.deltaTime / curlDrain;
        }
        
        else
        {
            isCurled = false;
        }

    }
}
