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
    public ParticleSystem ps;

    public GameObject[] lights = new GameObject[20];

    public bool isDead;

 

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Fog").GetComponent<ParticleSystem>();
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


    public IEnumerable Shake (float duration, float magnitude)
    {
        Vector3 originalPos = head.transform.parent.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            head.transform.parent.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;

        }

        head.transform.parent.position = originalPos;
    }

    public void Rate()
    {
        currentSanity = currentSanity + ((lightIntensity) - 5) * 0.06;
        currentSanity = 100;
        if (currentSanity >= 100)
        {
            //Can't have more than 100% sanity
            currentSanity = 100;
        }

        if (currentSanity > 50)
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve(0f, 0f);
            main.startLifetime = new ParticleSystem.MinMaxCurve(0f, 0f);
        }
            if (currentSanity < 50)
        {
            var main = ps.main;
            main.startSize = new ParticleSystem.MinMaxCurve((float)(3.2 - currentSanity / 100 * 3), (float)(3.2 - currentSanity / 100 * 3));
            // main.startLifetime = Vector3.Distance(main.transform.position, target.position) / main.startSpeed;
            main.startLifetime = new ParticleSystem.MinMaxCurve((float)(1 + currentSanity / 100 * 15), (float)(1 + currentSanity / 100 * 15));
            main.startSpeed = new ParticleSystem.MinMaxCurve((float)(3 - currentSanity / 100 * 3), (float)(1 - currentSanity / 100 * 3));
        }
        //Debug.Log(currentSanity);


        if (lightIntensity < 5)
        {
            //Signal the player that he fears the dark
        }

        if (currentSanity < 35)
        {
            //Signal the player that he is dying
            Shake(20, 3);
            Debug.Log("shake");
        }

        if (currentSanity <= 0 && !isDead)
        {
            // It should die.
            currentSanity = 0;
            Death();
        }
    }
    void Death()
    {
        //Annimations and triggers checkpoint
        isDead = true;
        currentSanity = 0;
        //var fade = GameObject.Find("Main Camera").GetComponent<FadeEvent>();
        //fade.changebool();
        Application.LoadLevel(Application.loadedLevel);
        //fade.changebool();

    }
}
