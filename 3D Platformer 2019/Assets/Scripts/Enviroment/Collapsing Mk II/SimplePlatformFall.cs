using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformFall : CollapsingMkII
{
    public Vector3 place;//start position

    private void Start()
    {
        place = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.down * fallSpeed) * Time.deltaTime;
    }

}
