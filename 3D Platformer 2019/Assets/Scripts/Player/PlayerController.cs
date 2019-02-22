using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// Foreword: Apologies for refactoring without permission.
    /// I tried to get it working as it was without any change, but I couldn't.
    /// I'm sorry.
    #region Variables
    public Rigidbody rigid;
    public Pickup trapTrigger;

    public GameObject curled;
    public GameObject uncurled;
    
    public float maxCurlTime = 1f, curCurlTime = 1f;
    public float curlDrain;
    public float curlRecharge;
    public bool isCurled;
    public bool curlLock;

    public Slider cooldownSlider;
    #endregion

    #region Functions 'n' Methods
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        curled = GameObject.Find("Curled");
        uncurled = GameObject.Find("Uncurled");
        isCurled = false;

        curlLock = false;
        curCurlTime = maxCurlTime;

        rigid = GetComponent<Rigidbody>();
        trapTrigger = GameObject.FindGameObjectWithTag("Pickup").GetComponent<Pickup>();
        cooldownSlider.maxValue = curCurlTime;
    }

    // Update is called once per frame, if MonoBehaviour is enabled
    void Update()
    {
        #region Respawning
        if (transform.position.y <= -5)
        {
            transform.position = Vector3.zero;
            rigid.velocity = Vector3.zero;
        }
        #endregion

        #region Curl Time Stuff
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
        if (curlLock)   // !!! Add to 'curCurlTime < 0'? !!!
            isCurled = false;

        // If our curCurlTime is fully recharged, then end curlLock (we can curl again).
        if (curCurlTime == maxCurlTime)
            curlLock = false;

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
        #endregion
        
        if (trapTrigger.isTrap)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // Slider shows cooldownTimer.
        cooldownSlider.value = curCurlTime;
    }

    public void Curl()
    {
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
    #endregion
}
