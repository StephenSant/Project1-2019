using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    #region Variables
    public bool isTrap = false;
    [Header("Components")]
    public MeshRenderer mesh;
    public Light glow;
    public CollapsingMkII startTrap;
    #endregion

    #region Functions 'n' Methods

    // Where we get our components.
    #region void Start()
    // Start is called just before any of the Update methods is called the first time
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        glow = GetComponentInChildren<Light>();
        startTrap = GameObject.Find("CollapseManager").GetComponent<CollapsingMkII>();
    }
    #endregion

    // Where we wait for the player to take the bait.
    #region void OnTriggerEnter(Collider col)
    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider col)
    {
        // When the player touches it, activate the trap (and disable the object's rendering stuff).
        if (col.tag == "Player" && isTrap == false)
        {
            isTrap = true;
            startTrap.StartCoroutine(startTrap.Test());
            mesh.enabled = false;
            glow.enabled = false;
            Debug.Log("Trap!");
        }
    }  
    #endregion
    #endregion
}
