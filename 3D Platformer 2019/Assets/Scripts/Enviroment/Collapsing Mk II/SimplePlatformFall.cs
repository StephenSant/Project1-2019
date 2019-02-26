using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformFall : CollapsingMkII
{
    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.down * fallSpeed) * Time.deltaTime;
    }
}
