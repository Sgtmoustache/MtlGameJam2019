using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    public int objectID;

    public override void Interact(GameObject player, bool input)
    {
        if (input)
        {
            Inventory.Collect(objectID);
            Debug.Log("You collected item #" + objectID);
            Destroy(this.gameObject);
        }
    }

    public override void OnStart()
    {
        objectID = Inventory.RegisterItem();
    }
}
