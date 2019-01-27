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
            Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>(),input);

            if (input)
            {
                currentActive = TypeOfAction.PUSHABLE;
                player.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

                transform.parent = player.transform;
                RaycastHit hit;

                Vector3 Direction = Vector3.Normalize(transform.position - player.transform.GetChild(3).transform.position);
                Ray rayFeet = new Ray(player.transform.GetChild(3).transform.position, Direction);

                if (Physics.Raycast(rayFeet, out hit, 3))
                {
                    Vector3 test = (transform.position + Direction * (1.2f - Vector3.Distance(player.transform.position, hit.point)));
                    transform.position = new Vector3(test.x, 0, test.z);
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
