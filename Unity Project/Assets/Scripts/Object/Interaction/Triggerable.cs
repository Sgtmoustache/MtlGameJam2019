using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : Interactable
{
    public int objectID = -1;
    private bool isAnimated = false;
    private bool isToggled = false;

    public override void Interact(GameObject player, bool input)
    {
        try
        {
            if(input && !isToggled)
            {
                //TOFIX
                //player.GetComponentInChildren<Animator>().SetBool("Grab", true);

                if (objectID == -1 || Inventory.HasCollected(objectID))
                {
                    var animator = GetComponentInParent<Animator>();
                    if (animator != null)
                    {
                        isToggled = true;
                        player.GetComponentInChildren<Animator>().SetBool("Grab", true);
                        animator.SetBool("Trigger", true);
                        Debug.Log("You triggered " + gameObject.name);
                    }
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
