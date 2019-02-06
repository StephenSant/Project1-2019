using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurledMovement : MonoBehaviour
{

    public float moveSpeed=15;

    private Transform parent;

    // Use this for initialization
    void Start()
    {
        //Remember to get everything from the parent
        parent = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        parent.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        parent.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
    }
}
