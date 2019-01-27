using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : Interactable
{
    public AudioClip DragingSound;
    private AudioSource audioSource;

    public override void Interact(GameObject player, bool input)
    {
        try
        {
            if (input)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = player.transform;

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
                GetComponent<Rigidbody>().isKinematic = false;
                audioSource.Stop();
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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = DragingSound;
        audioSource.loop = true;

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
