using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingMkII : MonoBehaviour
{
    // Due to SimplePlatformFall.cs relying on inheritance, 'time' and 'fallSpeed' have to be set here.
    // Trying to set the values in the inspector causes the platform fall to fail. I'm not sure why.
    [Header("SET FALL SPEED INSIDE SCRIPT!")]
    public float fallRate;

    // DO NOT CHANGE ACCESS TYPE!!  ↓ You can change value though.
    public static float fallSpeed = .75f; // ← I am not proud of this solution.
    
    // Array of our platforms.
    public GameObject[] platforms;

    /// private Pickup trapStart; // Upon review, I don't need this.
    ///
    /// // Use this for initialization
    /// void Start()
    /// {
    ///     // trapStart = GameObject.FindGameObjectWithTag("Pickup").GetComponent<Pickup>();
    /// }

    public void ResetPlatforms()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<SimplePlatformFall>().enabled = false;
            platforms[i].transform.position = platforms[i].GetComponent<SimplePlatformFall>().place;
        }
        
    }

    public IEnumerator Test()
    {
        // For loop to add SimplePlatformFall.cs to each platform in order every (time) seconds.
        for (int i = 0; i < platforms.Length; i++)
        {

            if (platforms[i].GetComponent<SimplePlatformFall>() == null)
            {
                platforms[i].AddComponent<SimplePlatformFall>();
            }
            else
            {
                platforms[i].GetComponent<SimplePlatformFall>().enabled = true;
            }
            yield return new WaitForSeconds(fallRate);
        }
    }
}
