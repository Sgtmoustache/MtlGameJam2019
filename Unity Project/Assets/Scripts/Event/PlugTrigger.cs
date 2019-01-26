using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugTrigger: TriggerObject
{

    public bool hasNightLight;

    public override void TriggerEffect()
    {

        if (Inventory.HasNightlight() || hasNightLight)
        {
            if (hasNightLight) Debug.Log("Enfant Jesus");
            hasNightLight = !hasNightLight;
            Inventory.ChangeNight();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        hasNightLight = false;
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
