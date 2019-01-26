using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : Interactable
{
    public override void Interact(GameObject player, bool input)
    {
        Debug.Log(input);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
