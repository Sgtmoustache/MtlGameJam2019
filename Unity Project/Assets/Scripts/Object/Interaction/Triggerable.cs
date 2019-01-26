using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : Interactable
{
    public int objectID;

    public override void Interact(GameObject player, bool input)
    {
        try
        {
            if (input && Inventory.HasCollected(objectID))
            {
                var animator = GetComponentInParent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("Trigger", true);
                    Debug.Log("You triggered " + gameObject.name);
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public override void OnStart()
    {
    }
}
