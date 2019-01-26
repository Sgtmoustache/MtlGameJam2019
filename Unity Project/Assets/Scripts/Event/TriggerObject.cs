using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public abstract void TriggerEffect();

    private void OnTriggerEnter(Collider other)
    {
        TriggerEffect();
    }
}
