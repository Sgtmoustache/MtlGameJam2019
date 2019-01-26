using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : MonoBehaviour
{
    public int triggerCount = 0;
    private int currentCount = 0;
    public bool collisionTrigger = true;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public abstract void TriggerEffect();

    private void OnTriggerEnter(Collider other)
    {
        if (collisionTrigger && (currentCount < triggerCount || triggerCount < 0))
        {
            TriggerEffect();
            currentCount++;
        }
            
    }
}
