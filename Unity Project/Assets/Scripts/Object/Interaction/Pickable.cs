using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{

    public override void Interact(GameObject player, bool input)
    {
        if (input)
        {
            player.GetComponentInChildren<Animator>().SetBool("Grab", true);
            Inventory.ChangeNight();
            Debug.Log("You collected item light");
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    public override void OnStart()
    {
        
    }

    

}
