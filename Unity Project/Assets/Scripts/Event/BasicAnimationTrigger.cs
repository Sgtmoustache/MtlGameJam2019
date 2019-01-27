using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimationTrigger : TriggerObject
{
    public float audioDelay = 0;
    public AudioClip AnimationSound;

    public override void TriggerEffect(GameObject player = null)
    {
        var animator = GetComponentInParent<Animator>();
        if (animator != null)
        {
            animator.SetBool("Trigger", true);
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(audioDelay);
        if (AnimationSound != null)
            GetComponent<AudioSource>().PlayOneShot(AnimationSound);
    }
}
