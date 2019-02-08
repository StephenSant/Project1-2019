using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsing : MonoBehaviour
{
    public Transform startPlatform;
    public Transform endPlatform;

    public float timeBetweenCollape = 5;

    private float collapeNum;


    public Transform[] platforms;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
            platforms = GetComponentsInChildren<Transform>();

        collapeNum = endPlatform.transform.position.z;
        coroutine = Test();
        Collape();
    }


    void Collape()
    {
        StartCoroutine(coroutine);
    }

    IEnumerator Test()
    {
        for (int i = platforms.Length - 1; i >= 1; i--)
        {
            Debug.Log(platforms[i] + " is collapsing!");
            yield return new WaitForSeconds(timeBetweenCollape);
        }
    }
}
