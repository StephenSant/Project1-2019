using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsing : MonoBehaviour
{
    public Transform startPlatform;
    public Transform endPlatform;

    public float timeBetweenCollape = 5;

    private float collapeNum;

    private int platformCollaping;

    public float fallSpeed = 500;

    public Transform[] platforms;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        platforms = GetComponentsInChildren<Transform>();
        collapeNum = endPlatform.transform.position.z;
        coroutine = Test();
    }


    public void StartCollape()
    {
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        platforms[platformCollaping].position += (Vector3.down * fallSpeed) * Time.deltaTime;
    }

    IEnumerator Test()
    {
        for (int i = platforms.Length - 1; i >= 1; i--)
        {
            platformCollaping = i;
            yield return new WaitForSeconds(timeBetweenCollape);
        }
    }
}
