using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SanityController : MonoBehaviour
{

    public int startingSanity = 100;
    public int currentSanity;
    public int lightIntensity;

    public GameObject player;
    public GameObject[] lights = new GameObject[20];

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        currentSanity = startingSanity;
        lights = GameObject.FindGameObjectsWithTag("Light");
        isDead = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lightIntensity = 0;
        foreach (GameObject l in lights)
        {
            //if no object is in between
            if (Physics.Linecast(transform.position, l.transform.position))
            {

                //shenaniggans to calculate the ammount of light on the player
                float distance = Vector3.Distance(l.transform.position, player.transform.position);
                Light currentLight = l.GetComponent<Light>();
                lightIntensity += (int)(currentLight.intensity * currentLight.range / distance);
                Debug.Log(lightIntensity);
            }
        }
        Rate();
    }

    public void Rate()
    {
        currentSanity = currentSanity + (lightIntensity) - 25;

        if (currentSanity >= 100)
        {
            //Can't have more than 100% sanity
            currentSanity = 100;
        }

        //Debug.Log(currentSanity);

        if (lightIntensity < 5)
        {
            //Signal the player that he fears the dark
        }

        if (currentSanity < 35)
        {
            //Signal the player that he is dying
        }

        if (currentSanity <= 0 && !isDead)
        {
            // It should die.
            Death();
        }
    }
    void Death()
    {
        //Annimations and triggers checkpoint
        isDead = true;
    }
}
