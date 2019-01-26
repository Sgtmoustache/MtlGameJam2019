using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : TriggerObject
{
    public Light light;
    public float intensity;
    public bool isOpen = false;

    public override void TriggerEffect(GameObject player = null)
    {
        if (currentCount < triggerCount || triggerCount <= 0)
        {
            isOpen = !isOpen;
            light.enabled = isOpen;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        light.intensity = intensity;
        light.enabled = isOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
