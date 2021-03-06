﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerMkII : MonoBehaviour
{
    /// Foreword: Apologies for refactoring without permission.
    /// I tried to get it working as it was without any change, but I couldn't.
    /// I'm sorry.
    #region Variables
    public Rigidbody rigid;
    public Pickup trapTrigger;

    public GameObject curled;
    public SphereCollider colCurled;
    public GameObject uncurled;
    public CapsuleCollider colUncurled;

    public float maxCurlTime = 1f, curCurlTime = 1f;
    public float curlDrain;
    public float curlRecharge;
    public bool isCurled;
    public bool curlLock;

    public Slider cooldownSlider;

    //where its gonna respawn
    public Vector3 spawnPoint;

    //

    public float moveSpeed = 5, curlSpeed = 15;
    public float jumpHeight = 3;
    // Are we touching the ground?
    public bool grounded;

    // moveDirection is the neutral analogue input position (inputs handled in PlayerInput.cs).
    private Vector3 moveDirection = Vector3.zero;
    #endregion

    #region Functions 'n' Methods
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trapTrigger = GameObject.FindGameObjectWithTag("Pickup").GetComponent<Pickup>();

        curled = GameObject.Find("Curled");
        colCurled = GetComponent<SphereCollider>();
        uncurled = GameObject.Find("Uncurled");
        colUncurled = GetComponent<CapsuleCollider>();

        curCurlTime = maxCurlTime;
        isCurled = false;
        curlLock = false;

        cooldownSlider = GameObject.Find("Slider").GetComponent<Slider>();
        cooldownSlider.maxValue = curCurlTime;
    }

    void Respawn()
    {
        transform.position = spawnPoint;
        rigid.velocity = Vector3.zero;
        GameObject.Find("CollapseManager").GetComponent<CollapsingMkII>().ResetPlatforms();
    }

    // Update is called once per frame, if MonoBehaviour is enabled
    void Update(){
        #region Set Rotation
        if (trapTrigger.isTrap)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        #endregion

        #region Dying
        if (transform.position.y <= -5)
        {
            Respawn();
        }
        #endregion

        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

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

        if (isCurled)
        {
            colCurled.enabled = true;
            colUncurled.enabled = false;
        }
        else
        {
            colCurled.enabled = false;
            colUncurled.enabled = true;
        }

        if (trapTrigger.isTrap)
        {
            spawnPoint = new Vector3(0, 9, 157);
        }
        else
        {
            spawnPoint = Vector3.zero;
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

    public void UpdateMove()
    {
        // transform.Translate(moveDirection * Time.deltaTime);
        Vector3 force = new Vector3(moveDirection.x, rigid.velocity.y, moveDirection.z);
        rigid.velocity = force;
    }

    public void Move(float inputH, float inputV)
    {
        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = transform.TransformDirection(moveDirection);
        if (isCurled == false)
            moveDirection *= moveSpeed;
        else
            moveDirection *= curlSpeed;
    }

    public void Jump()
    {
        if (grounded)
        {
            rigid.velocity = ((Vector3.up * 100) * Time.deltaTime * jumpHeight);
        }
    }
    #endregion

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.SetParent(col.transform);
        }
    }

    // OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.SetParent(null);
        }
    }
}
