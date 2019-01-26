using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : MonoBehaviour
{
    public int triggerCount = 1;
    private int currentCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public abstract void TriggerEffect();

    private void OnTriggerEnter(Collider other)
    {
        if (currentCount < triggerCount)
        {
            TriggerEffect();
            currentCount++;
        }
            
    }
}
