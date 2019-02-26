using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingMkII : MonoBehaviour
{
    public Pickup trapStart;
    public float time = 1f;
    public float fallSpeed = 2f;
    public int platformIndex;
    public GameObject[] platforms;

    // Use this for initialization
    void Start()
    {
        trapStart = GameObject.FindGameObjectWithTag("Pickup").GetComponent<Pickup>();
    }

    public IEnumerator Test()
    {
        //if (trapStart.isTrap)
        //{
        for (int i = 0; i < platforms.Length; i++)
        {
            print(i);
            platforms[i].AddComponent<SimplePlatformFall>();
            yield return new WaitForSeconds(time);
        }
        //}
    }
}
