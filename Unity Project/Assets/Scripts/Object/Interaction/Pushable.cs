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

                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY
                    | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
                var animator = GetComponentInParent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("Grab", true);
                    Debug.Log("You grabbed " + gameObject.name);
                }
                if (Input.GetKeyDown("w"))
                {
                    animator.SetBool("Push", true);
                    Debug.Log("You pushed " + gameObject.name);
                }
                if (Input.GetKeyDown("s"))
                {
                    animator.SetBool("Pull", true);
                    Debug.Log("You pulled " + gameObject.name);
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
        //throw new System.NotImplementedException();
    }
}
