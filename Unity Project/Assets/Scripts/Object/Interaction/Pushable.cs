using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : Interactable
{
    public override void Interact(GameObject player, bool input)
    {
        try
        {
            if (input)
            {

                transform.parent = player.transform;
                player.SendMessage("StopSideWay", false);
                player.SendMessage("StopRotation", false);

                if (Input.GetKeyDown("w"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Push", true);
                    Debug.Log("You pushed " + gameObject.name);
                }
                if (Input.GetKeyUp("w"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Push", false);
                    Debug.Log("You stop pushing " + gameObject.name);
                }
                if (Input.GetKeyDown("s"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Pull", true);
                    Debug.Log("You pulled " + gameObject.name);
                }
                if (Input.GetKeyUp("s"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Pull", false);
                    Debug.Log("You stop pulling " + gameObject.name);
                }
            }
            if (!input)
            {
                player.GetComponentInChildren<Animator>().SetBool("Push", false);
                player.GetComponentInChildren<Animator>().SetBool("Pull", false);
                Debug.Log("Tu marches");
                transform.parent = null;
                print("Left click was released");
                player.SendMessage("StopSideWay", true);
                player.SendMessage("StopRotation", true);
            }

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public override void OnStart()
    {
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
