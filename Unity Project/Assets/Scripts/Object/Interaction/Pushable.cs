using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : Interactable
{
    public AudioClip DragingSound;
    private AudioSource audioSource;
    public Joint joint;

    private bool first_pass;

    public override void Interact(GameObject player, bool input)
    {
        Rigidbody r_player = player.GetComponent<Rigidbody>();
        Rigidbody r_object = this.GetComponent<Rigidbody>();

        try
        {
            if (input)
            {
                  currentActive = TypeOfAction.PUSHABLE;

                if (first_pass)
                {
                    player.gameObject.AddComponent<FixedJoint>();
                    player.gameObject.GetComponent<FixedJoint>().connectedBody = r_object;
                    first_pass = false;
                    r_object.mass = 1;
                }

                player.SendMessage("StopSideWay", false);
                player.SendMessage("StopRotation", false);

                if (Input.GetKey("w"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Push", true);
                    Debug.Log("You pushed " + gameObject.name);
                    if(!audioSource.isPlaying)
                        audioSource.Play();
                }
                if (!Input.GetKey("w"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Push", false);
                    Debug.Log("You stop pushing " + gameObject.name);
                    audioSource.Stop();
                }
                if (Input.GetKey("s"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Pull", true);
                    Debug.Log("You pulled " + gameObject.name);
                    if (!audioSource.isPlaying)
                        audioSource.Play();
                }
                if (!Input.GetKey("s"))
                {
                    player.GetComponentInChildren<Animator>().SetBool("Pull", false);
                    Debug.Log("You stop pulling " + gameObject.name);
                    audioSource.Stop();
                }
            }
            if (!input)
            {
                Destroy(player.GetComponent<ConfigurableJoint>());
                currentActive = TypeOfAction.NOTHING;
                player.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                GetComponent<Rigidbody>().isKinematic = false;
                audioSource.Stop();
                player.GetComponentInChildren<Animator>().SetBool("Push", false);
                player.GetComponentInChildren<Animator>().SetBool("Pull", false);
                Debug.Log("Tu marches");
                transform.parent = null;
                print("Left click was released");
                player.SendMessage("StopSideWay", true);
                player.SendMessage("StopRotation", true);
                first_pass = true;
                r_object.mass = 1000;
                Destroy(player.gameObject.GetComponent<FixedJoint>());
            }

        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public override void OnStart()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = DragingSound;
        audioSource.loop = true;
        first_pass = true;

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
