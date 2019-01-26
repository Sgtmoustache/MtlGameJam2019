using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : TriggerObject
{
    public Light light;
    public float intensity;
    public bool isOpen = false;
    private AudioSource audioSource;
    public AudioClip switchSound;

    public override void TriggerEffect(GameObject player = null)
    {
        if (currentCount < triggerCount || triggerCount <= 0)
        {
            isOpen = !isOpen;
            light.enabled = isOpen;
            audioSource.PlayOneShot(switchSound);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        light.intensity = intensity;
        light.enabled = isOpen;

        if (GetComponent<AudioSource>() == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
