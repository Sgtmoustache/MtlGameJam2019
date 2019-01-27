using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTrigger : TriggerObject
{
    public AudioClip soundEffect;

    [Range(0,1)]
    public float volume;
    public override void TriggerEffect(GameObject player = null)
    {
        if (currentCount < triggerCount || triggerCount <= 0)
        {
            if(soundEffect != null)
                GetComponent<AudioSource>().PlayOneShot(soundEffect, volume);
        }
    }
}
