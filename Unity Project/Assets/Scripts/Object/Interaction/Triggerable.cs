using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : Interactable
{
    public int objectID = -1;
    private bool isToggled = false;
    public AudioClip AnimationSound;
    private AudioSource audioSource;
    public float audioDelay = 0;

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
                        StartCoroutine(PlaySound());
                        
                        Debug.Log("You triggered " + gameObject.name);
                    }

                    //If has a TriggerObject component.
                    foreach (TriggerObject trigger in GetComponents<TriggerObject>())
                    {
                        trigger.TriggerEffect(player);
                    }
                }
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(audioDelay);
        audioSource.PlayOneShot(AnimationSound);
    }
    

    public override void OnStart()
    {
        if (GetComponent<AudioSource>() == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
}
