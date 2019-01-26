using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SanityController : MonoBehaviour
{

    public GameObject head;

    public double startingSanity = 100;
    public double currentSanity;
    public double lightIntensity;

    
    public GameObject[] lights = new GameObject[20];

    bool isDead;

 

    // Start is called before the first frame update
    void Start()
    {
        currentSanity = startingSanity;
        lights = GameObject.FindGameObjectsWithTag("Light");
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        lightIntensity = 0;
        foreach (GameObject l in lights)
        {
            //if no object is in between
            if (!Physics.Linecast(head.transform.position, l.transform.position))
            {

                //shenaniggans to calculate the ammount of light on the player
                double distance = Vector3.Distance(l.transform.position, head.transform.position);
                Light currentLight = l.GetComponent<Light>();
                lightIntensity += (currentLight.intensity * currentLight.range / distance);
            }
        }
        Rate();
    }

    public void Rate()
    {
        currentSanity = currentSanity + ((lightIntensity) - 5) * 0.1;

        if (currentSanity >= 100)
        {
            //Can't have more than 100% sanity
            currentSanity = 100;
        }

        //Debug.Log(currentSanity);

        if (lightIntensity < 2)
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
            currentSanity = 0;
            //Death();
        }
    }
    void Death()
    {
        //Annimations and triggers checkpoint
        isDead = true;
        currentSanity = 0;
    }
}
